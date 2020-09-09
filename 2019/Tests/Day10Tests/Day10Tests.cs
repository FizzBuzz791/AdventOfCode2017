using System.Collections.Generic;
using System.Drawing;
using NAoCHelper;
using NSubstitute;
using NUnit.Framework;
using Shouldly;
using Solutions.Day10;

namespace Tests.Day10Tests
{
	public class Day10Tests
	{
		[TestCaseSource(nameof(Part1Cases))]
		public void FindsBestLocation(string map, Point expectedLocation, int expectedAsteroids)
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync().Returns(map);

			var day10 = new Solution(puzzle);

			// Act
			string bestLocation = day10.SolvePart1();

			// Assert
			bestLocation.ShouldBe($"Part 1: {expectedAsteroids}");
		}

		private static IEnumerable<object[]> Part1Cases()
		{
			yield return new object[] { ".#..#\n.....\n#####\n....#\n...##", new Point(3, 4), 8 };
			yield return new object[] { "......#.#.\n#..#.#....\n..#######.\n.#.#.###..\n.#..#.....\n..#....#.#\n#..#....#.\n.##.#..###\n##...#..#.\n.#....####", new Point(5, 8), 33 };
			yield return new object[] { "#.#...#.#.\n.###....#.\n.#....#...\n##.#.#.#.#\n....#.#.#.\n.##..###.#\n..#...##..\n..##....##\n......#...\n.####.###.", new Point(1, 2), 35 };
			yield return new object[] { ".#..#..###\n####.###.#\n....###.#.\n..###.##.#\n##.##.#.#.\n....###..#\n..#.#..#.#\n#..#.#.###\n.##...##.#\n.....#.#..", new Point(6, 3), 41 };
			yield return new object[] { ".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##", new Point(11, 13), 210 };
		}
		
		[TestCaseSource(nameof(Part2Cases))]
		public void Finds200ThAsteroid(string map, Point expectedLocation)
		{
			// Arrange
			var puzzle = Substitute.For<IPuzzle>();
			puzzle.GetInputAsync().Returns(map);

			var day10 = new Solution(puzzle);
			
			// Act
			day10.SolvePart1(); // Need to solve part 1 to get the "best location".
			string targetAsteroid = day10.SolvePart2();

			// Assert
			targetAsteroid.ShouldBe($"Part 2: {expectedLocation.X * 100 + expectedLocation.Y}");
		}
		
		private static IEnumerable<object[]> Part2Cases()
		{
			yield return new object[] { ".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##", new Point(8, 2) };
		}
	}
}