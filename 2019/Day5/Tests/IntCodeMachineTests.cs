using Day5;
using NUnit.Framework;
using Shouldly;

namespace Day5Tests
{
    public class IntCodeMachineTests
    {
        [TestCase(new int[] { 1, 0, 0, 0, 99 }, new int[] { 2, 0, 0, 0, 99 })]
        [TestCase(new int[] { 2, 3, 0, 3, 99 }, new int[] { 2, 3, 0, 6, 99 })]
        [TestCase(new int[] { 2, 4, 4, 5, 99, 0 }, new int[] { 2, 4, 4, 5, 99, 9801 })]
        [TestCase(new int[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 }, new int[] { 30, 1, 1, 4, 2, 5, 6, 0, 99 })]
        [TestCase(new int[] { 1, 9, 10, 3, 2, 3, 11, 0, 99, 30, 40, 50 }, new int[] { 3500, 9, 10, 70, 2, 3, 11, 0, 99, 30, 40, 50 })]
        [TestCase(new int[] { 3, 0, 4, 0, 99 }, new int[] { 1, 0, 4, 0, 99 })] // "Outputs whatever the input is"
        [TestCase(new int[] { 1002, 4, 3, 4, 33 }, new int[] { 1002, 4, 3, 4, 99 })]
        [TestCase(new int[] { 1101, 100, -1, 4, 0 }, new int[] { 1101, 100, -1, 4, 99 })]
        public void IntCodeMachineReturnsExpectedResult(int[] initialState, int[] expectedResult)
        {
            // Arrange
            var computer = new IntCodeMachine(initialState);

            // Act
            computer.Execute();

            // Assert
            computer.Memory.ShouldBe(expectedResult);
        }
    }
}