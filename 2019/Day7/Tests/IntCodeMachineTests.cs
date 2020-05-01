using System.Linq;
using Day7;
using NUnit.Framework;
using Shouldly;

namespace Day7Tests
{
    public class IntCodeMachineTests
    {
        [TestCase(new int[] { 1, 0, 0, 0, 99 }, new int[] { 2, 0, 0, 0, 99 })]
        [TestCase(new int[] { 2, 3, 0, 3, 99 }, new int[] { 2, 3, 0, 6, 99 })]
        [TestCase(new int[] { 2, 4, 4, 5, 99, 0 }, new int[] { 2, 4, 4, 5, 99, 9801 })]
        [TestCase(new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new int[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [TestCase(new int[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 })]
        [TestCase(new int[] { 1002, 4, 3, 4, 33 }, new int[] { 1002, 4, 3, 4, 99 })]
        [TestCase(new int[] { 1101, 100, -1, 4, 0 }, new int[] { 1101, 100, -1, 4, 99 })]
        public void SimpleTests(int[] initialState, int[] expectedResult)
        {
            // Arrange
            var computer = new IntCodeMachine(initialState);

            // Act
            computer.Execute();

            // Assert
            computer.Memory.ShouldBe(expectedResult);
        }

        [TestCase(new int[] { 3, 0, 4, 0, 99 }, 43, new int[] { 43, 0, 4, 0, 99 }, new string[] { "43", "Halt" })] // Outputs the input.
        [TestCase(new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, -1, 8 }, 43, new int[] { 3, 9, 8, 9, 10, 9, 4, 9, 99, 0, 8 }, new string[] { "0", "Halt" })] // Compare input to 8, output truthy/falsey
        [TestCase(new int[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, -1, 8 }, 7, new int[] { 3, 9, 7, 9, 10, 9, 4, 9, 99, 1, 8 }, new string[] { "1", "Halt" })] // Input < 8, output truthy/falsey
        [TestCase(new int[] { 3, 3, 1108, -1, 8, 3, 4, 3, 99 }, 43, new int[] { 3, 3, 1108, 0, 8, 3, 4, 3, 99 }, new string[] { "0", "Halt" })] // Compare input to 8, output truthy/falsey
        [TestCase(new int[] { 3, 3, 1107, -1, 8, 3, 4, 3, 99 }, 7, new int[] { 3, 3, 1107, 1, 8, 3, 4, 3, 99 }, new string[] { "1", "Halt" })] // Input < 8, output truthy/falsey
        public void IOTests(int[] initialState, int input, int[] expectedResult, string[] expectedOutput)
        {
            // Arrange
            var computer = new IntCodeMachine(initialState, input);

            // Act
            computer.Execute();

            // Assert
            computer.Memory.ShouldBe(expectedResult);
            computer.Outputs.ShouldBe(expectedOutput.ToList());
        }

        [TestCase(new int[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 43, new int[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, 43, 1, 1, 9 }, new string[] { "1", "Halt" })] // Output truthy/falsey if input is non-zero
        [TestCase(new int[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 43, new int[] { 3, 3, 1105, 43, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, new string[] { "1", "Halt" })] // Output truthy/falsey if input is non-zero
        [TestCase(new int[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 0, 0, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, 43, new int[] { 3, 21, 1008, 21, 8, 20, 1005, 20, 22, 107, 8, 21, 20, 1006, 20, 31, 1106, 0, 36, 98, 1001, 43, 1002, 21, 125, 20, 4, 20, 1105, 1, 46, 104, 999, 1105, 1, 46, 1101, 1000, 1, 20, 4, 20, 1105, 1, 46, 98, 99 }, new string[] { "1001", "Halt" })] // Output 999 if input < 8, 1000 if input == 8, 1001 if input > 8
        public void JumpTests(int[] initialState, int input, int[] expectedResult, string[] expectedOutput)
        {
            // Arrange
            var computer = new IntCodeMachine(initialState, input);

            // Act
            computer.Execute();

            // Assert
            computer.Memory.ShouldBe(expectedResult);
            computer.Outputs.ShouldBe(expectedOutput.ToList());
        }
    }
}