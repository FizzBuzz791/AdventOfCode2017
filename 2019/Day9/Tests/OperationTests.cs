using System.Collections.Generic;
using System.Numerics;
using Day9;
using NUnit.Framework;
using Shouldly;

namespace Day9Tests
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
        }

        private static IEnumerable<object[]> ConstructorCases()
        {
            yield return new object[] { new BigInteger(1002), OpCode.Multiply, Mode.Position, Mode.Immediate, Mode.Position };
        }
    }
}