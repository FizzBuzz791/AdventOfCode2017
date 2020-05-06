using System.Collections.Generic;
using System.Drawing;
using Day10;
using NUnit.Framework;
using Shouldly;

namespace Day10Tests
{
    public class Part1Tests
    {
        [TestCaseSource(nameof(Part1Cases))]
        public void FindsBestLocation(string map, Point expectedLocation, int expectedAsteroids)
        {
            // Arrange

            // Act
            var bestLocation = Program.Part1(map);

            // Assert
            bestLocation.Item1.ShouldBe(expectedLocation);
            bestLocation.Item2.ShouldBe(expectedAsteroids);
        }

        private static IEnumerable<object[]> Part1Cases()
        {
            yield return new object[] { ".#..#\n.....\n#####\n....#\n...##", new Point(3, 4), 8 };
            yield return new object[] { "......#.#.\n#..#.#....\n..#######.\n.#.#.###..\n.#..#.....\n..#....#.#\n#..#....#.\n.##.#..###\n##...#..#.\n.#....####", new Point(5, 8), 33 };
            yield return new object[] { "#.#...#.#.\n.###....#.\n.#....#...\n##.#.#.#.#\n....#.#.#.\n.##..###.#\n..#...##..\n..##....##\n......#...\n.####.###.", new Point(1, 2), 35 };
            yield return new object[] { ".#..#..###\n####.###.#\n....###.#.\n..###.##.#\n##.##.#.#.\n....###..#\n..#.#..#.#\n#..#.#.###\n.##...##.#\n.....#.#..", new Point(6, 3), 41 };
            yield return new object[] { ".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##", new Point(11, 13), 210 };
        }
    }
}