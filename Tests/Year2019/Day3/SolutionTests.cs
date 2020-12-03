using AdventOfCode.Year2019.Day3;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Tests.Year2019.Day3
{
    public class SolutionTests
    {
        [TestCase("R8,U5,L5,D3\nU7,R6,D4,L4", 6)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83", 159)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 135)]
        public void Part1CalculatesExpectedResult(string input, int expectedResult)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(_ => input);
            
            var day3 = new Solution(puzzle);

            // Act
            string closestCrossoverDistance = day3.SolvePart1();

            // Assert
            closestCrossoverDistance.ShouldBe($"Part 1: {expectedResult}");
        }
        
        [TestCase("R8,U5,L5,D3\nU7,R6,D4,L4", 30)]
        [TestCase("R75,D30,R83,U83,L12,D49,R71,U7,L72\nU62,R66,U55,R34,D71,R55,D58,R83", 610)]
        [TestCase("R98,U47,R26,D63,R33,U87,L62,D20,R33,U53,R51\nU98,R91,D20,R16,D67,R40,U7,R15,U6,R7", 410)]
        public void Part2CalculatesExpectedResult(string input, int expectedResult)
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(_ => input);
            
            var day3 = new Solution(puzzle);

            // Act
            string closestCrossoverDistance = day3.SolvePart2();

            // Assert
            closestCrossoverDistance.ShouldBe($"Part 2: {expectedResult}");
        }
    }
}