using NUnit.Framework;
using Shouldly;

namespace Day4Tests
{
    public class Tests
    {
        [TestCase(111111, true)]
        [TestCase(223450, false)]
        [TestCase(123789, false)]
        public void CanValidateNumbersCorrectly(int input, bool expectedResult)
        {
            // Arrange

            // Act
            var numberIsValid = Day4.Program.NumberIsValid(input);

            // Assert
            numberIsValid.ShouldBe(expectedResult);
        }
    }
}