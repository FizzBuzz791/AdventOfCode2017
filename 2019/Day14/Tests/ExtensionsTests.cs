using Day14;
using NUnit.Framework;
using Shouldly;

namespace Day14Tests
{
    public class ExtensionsTests
    {
        [TestCase(1.0, true)]
        [TestCase(1.1, false)]
        public void DeterminesWholeNumbersCorrectly(double number, bool expectedResult)
        {
            number.IsWholeNumber().ShouldBe(expectedResult);
        }
    }
}