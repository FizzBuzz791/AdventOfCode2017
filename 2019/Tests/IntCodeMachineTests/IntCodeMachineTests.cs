using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using NUnit.Framework;
using Shouldly;

namespace Tests.IntCodeMachineTests
{
    public class IntCodeMachineTests
    {
        [TestCaseSource(nameof(SimpleCases))]
        public void SimpleTests(BigInteger[] initialState, BigInteger[] expectedResult)
        {
            // Arrange
            var computer = new IntCodeMachine.IntCodeMachine(initialState);

            // Act
            computer.Execute(false);

            // Assert
            computer.Memory.ShouldBe(new IntCodeMachine.IntCodeMachine(expectedResult).Memory);
        }

        private static IEnumerable<object[]> SimpleCases()
        {
            yield return new object[] { new BigInteger[] { 1, 0, 0, 0, 99 }, new BigInteger[] { 2, 0, 0, 0, 99 } };
            yield return new object[] { new BigInteger[] { 2, 3, 0, 3, 99 }, new BigInteger[] { 2, 3, 0, 6, 99 } };
            yield return new object[] { new BigInteger[] { 2, 4, 4, 5, 99, 0 }, new BigInteger[] { 2, 4, 4, 5, 99, 9801 } };
            yield return new object[] { new BigInteger[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new BigInteger[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 } };
            yield return new object[] { new BigInteger[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new BigInteger[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 } };
            yield return new object[] { new BigInteger[] { 1002, 4, 3, 4, 33 }, new BigInteger[] { 1002, 4, 3, 4, 99 } };
            yield return new object[] { new BigInteger[] { 1101, 100, -1, 4, 0 }, new BigInteger[] { 1101, 100, -1, 4, 99 } };
        }

        [TestCaseSource(nameof(IOCases))]
        public void IOTests(BigInteger[] initialState, int input, BigInteger[] expectedResult, string[] expectedOutput)
        {
            // Arrange
            var computer = new IntCodeMachine.IntCodeMachine(initialState, new[] { input });

            // Act
            computer.Execute(false);

            // Assert
            computer.Memory.ShouldBe(new IntCodeMachine.IntCodeMachine(expectedResult).Memory);
            computer.Outputs.ShouldBe(expectedOutput.ToList());
        }

        private static IEnumerable<object[]> IOCases()
        {
            // Outputs the input.
            yield return new object[] { new BigInteger[] { 3, 0, 4, 0, 99 }, 43, new BigInteger[] { 43, 0, 4, 0, 99 }, new[] { "43", "Halt" } };
            // Compare input to 8, output truthy/falsey
            yield return new object[] { new BigInteger[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 43, new BigInteger[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, 0, 8 }, new[] { "0", "Halt" } };
            // Input < 8, output truthy/falsey
            yield return new object[] { new BigInteger[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 7, new BigInteger[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, 1, 8 }, new[] { "1", "Halt" } };
            // Compare input to 8, output truthy/falsey
            yield return new object[] { new BigInteger[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 43, new BigInteger[] { 3, 3, 1108, 0, 8, 3, 4, 3, 99 }, new[] { "0", "Halt" } };
            // Input < 8, output truthy/falsey
            yield return new object[] { new BigInteger[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 7, new BigInteger[] { 3, 3, 1107, 1, 8, 3, 4, 3, 99 }, new[] { "1", "Halt" } };
        }

        [TestCaseSource(nameof(JumpCases))]
        public void JumpTests(BigInteger[] initialState, int input, BigInteger[] expectedResult, string[] expectedOutput)
        {
            // Arrange
            var computer = new IntCodeMachine.IntCodeMachine(initialState, new[] { input });

            // Act
            computer.Execute(false);

            // Assert
            computer.Memory.ShouldBe(new IntCodeMachine.IntCodeMachine(expectedResult).Memory);
            computer.Outputs.ShouldBe(expectedOutput.ToList());
        }

        private static IEnumerable<object[]> JumpCases()
        {
            // Output truthy/falsey if input is non-zero
            yield return new object[] { new BigInteger[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 43, new BigInteger[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, 43, 1, 1, 9 }, new[] { "1", "Halt" } };
            // Output truthy/falsey if input is non-zero
            yield return new object[] { new BigInteger[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 43, new BigInteger[] { 3, 3, 1105, 43, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, new[] { "1", "Halt" } };
            // Output 999 if input < 8, 1000 if input == 8, 1001 if input > 8
            yield return new object[] { new BigInteger[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 43, new BigInteger[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 1001, 43, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, new[] { "1001", "Halt" } };
        }

        [TestCaseSource(nameof(RelativeBaseCases))]
        public void RelativeBaseTests(BigInteger[] initialState, int expectedRelativeBase, string[] expectedOutput)
        {
            // Arrange
            var computer = new IntCodeMachine.IntCodeMachine(initialState, new int[] { });

            // Act
            computer.Execute(false);

            // Assert
            computer.RelativeBase.ShouldBe(expectedRelativeBase);
            computer.Outputs.ShouldBe(expectedOutput.ToList());
        }

        private static IEnumerable<object[]> RelativeBaseCases()
        {
            // Simple increment of Relative Base
            yield return new object[] { new BigInteger[] { 109, 19, 99 }, 19, new[] { "Halt" } };
            // Quine
            yield return new object[] { new BigInteger[] { 109, 1, 204, -1, 1001, 100, 1, 100, 1008, 100, 16, 101, 1006, 101, 0, 99 }, 16, new[] { "109", "1", "204", "-1", "1001", "100", "1", "100", "1008", "100", "16", "101", "1006", "101", "0", "99", "Halt" } };
        }

        [TestCaseSource(nameof(BigIntegerCases))]
        public void BigIntegerTests(BigInteger[] initialState, string[] expectedOutput)
        {
            // Arrange
            var computer = new IntCodeMachine.IntCodeMachine(initialState, new int[] { });

            // Act
            computer.Execute(false);

            // Assert
            computer.Outputs.ShouldBe(expectedOutput.ToList());
        }

        private static IEnumerable<object[]> BigIntegerCases()
        {
            // Output 16-digit number
            yield return new object[] { new BigInteger[] { 1102, 34915192, 34915192, 7, 4, 7, 99, 0 }, new[] { "1219070632396864", "Halt" } };
            // Output 1125899906842624
            yield return new object[] { new BigInteger[] { 104, 1125899906842624, 99 }, new[] { "1125899906842624", "Halt" } };
        }

        [TestCaseSource(nameof(AdditionalCases))]
        public void AdditionalTests(BigInteger[] initialState, string[] expectedOutput)
        {
            // Arrange
            var computer = new IntCodeMachine.IntCodeMachine(initialState, new[] { 12345 });

            // Act
            computer.Execute(false);

            // Assert
            computer.Outputs.ShouldBe(expectedOutput.ToList());
        }

        private static IEnumerable<object[]> AdditionalCases()
        {
            yield return new object[] { new BigInteger[] { 109, -1, 4, 1, 99 }, new[] { "-1", "Halt" } };
            yield return new object[] { new BigInteger[] { 109, -1, 104, 1, 99 }, new[] { "1", "Halt" } };
            yield return new object[] { new BigInteger[] { 109, -1, 204, 1, 99 }, new[] { "109", "Halt" } };
            yield return new object[] { new BigInteger[] { 109, 1, 9, 2, 204, -6, 99 }, new[] { "204", "Halt" } };
            yield return new object[] { new BigInteger[] { 109, 1, 109, 9, 204, -6, 99 }, new[] { "204", "Halt" } };
            yield return new object[] { new BigInteger[] { 109, 1, 209, -1, 204, -106, 99 }, new[] { "204", "Halt" } };
            yield return new object[] { new BigInteger[] { 109, 1, 3, 3, 204, 2, 99 }, new[] { "12345", "Halt" } };
            yield return new object[] { new BigInteger[] { 109, 1, 203, 2, 204, 2, 99 }, new[] { "12345", "Halt" } };
        }
    }
}