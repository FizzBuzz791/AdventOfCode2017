using AdventOfCode.Year2020.Day4;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;

namespace Tests.Year2020.Day4
{
    public class SolutionTests
    {
        [Test]
        public void Part1ReturnsExpectedResult()
        {
            // Arrange
            var puzzle = Substitute.For<IPuzzle>();
            puzzle.GetInputAsync().Returns(_ => "ecl:gry pid:860033327 eyr:2020 hcl:#fffffd\nbyr:1937 iyr:2017 cid:147 hgt:183cm\n\niyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884\nhcl:#cfa07d byr:1929\n\nhcl:#ae17e1 iyr:2013\neyr:2024\necl:brn pid:760753108 byr:1931\nhgt:179cm\n\nhcl:#cfa07d eyr:2025 pid:166559648\niyr:2011 ecl:brn hgt:59in");

            var day4 = new Solution(puzzle);

            // Act
            string result = day4.SolvePart1();

            // Assert
            result.ShouldBe("Part 1: 2");
        }
    }
}