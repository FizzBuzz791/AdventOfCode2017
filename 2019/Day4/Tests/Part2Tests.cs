using NUnit.Framework;
using Shouldly;

namespace Day4Tests
{
    public class Part2Tests
    {
        [TestCase(112233, true)]
        [TestCase(123444, false)]
        [TestCase(111122, true)]
        public void CanValidateNumbersCorrectly(int input, bool expectedResult)
        {
            // Arrange

            // Act
            var numberIsValid = Day4.Program.NumberIsValid(input, true);

            // Assert
            numberIsValid.ShouldBe(expectedResult);
        }
    }
}