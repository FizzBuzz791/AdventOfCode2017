using Day3;
using NUnit.Framework;
using Shouldly;

namespace Day3Tests
{
    public class Part1Tests
    {
        [TestCase("R8,U5,L5,D3\nU7,R6,D4,L4", 6)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void Part1CalculatesExpectedResult(string input, int expectedResult)
        {
            // Arrange
            var crossovers = Program.FindCrossovers(input);

            // Act
            var closestCrossoverDistance = Program.Part1(crossovers);

            // Assert
            closestCrossoverDistance.ShouldBe(expectedResult);
        }
    }
}