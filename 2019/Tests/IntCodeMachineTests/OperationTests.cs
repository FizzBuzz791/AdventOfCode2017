using System.Collections.Generic;
using System.Numerics;
using IntCodeMachine;
using NUnit.Framework;
using Shouldly;

namespace Tests.IntCodeMachineTests
{
    public class OperationTests
    {
        [TestCaseSource(nameof(ConstructorCases))]
        public void CanSuccessfullyConstructOperationFromInteger(BigInteger input, OpCode expectedOpCode, Mode expectedMode1, Mode expectedMode2, Mode expectedMode3)
        {
            // Arrange

            // Act
            var operation = new Operation(input);

            // Assert
            operation.OpCode.ShouldBe(expectedOpCode);
            operation.FirstParameterMode.ShouldBe(expectedMode1);
            operation.SecondParameterMode.ShouldBe(expectedMode2);
            operation.ThirdParameterMode.ShouldBe(expectedMode3);
        }

        private static IEnumerable<object[]> ConstructorCases()
        {
            yield return new object[] { new BigInteger(1002), OpCode.Multiply, Mode.Position, Mode.Immediate, Mode.Position };
        }
    }
}