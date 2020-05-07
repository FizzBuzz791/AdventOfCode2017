using System.Collections.Generic;
using System.Drawing;
using Day10;
using NUnit.Framework;
using Shouldly;

namespace Day10Tests
{
    public class Part2Tests
    {
        [TestCaseSource(nameof(Part1Cases))]
        public void Finds200thAsteroid(string map, Point expectedLocation)
        {
            // Arrange
            var asteroids = Program.FindAsteroids(map);
            var bestLocation = new Point(11, 13);

            // Act
            var targetAsteroid = Program.Part2(bestLocation, asteroids);

            // Assert
            targetAsteroid.ShouldBe(expectedLocation);
        }

        private static IEnumerable<object[]> Part1Cases()
        {
            yield return new object[] { ".#..##.###...#######\n##.############..##.\n.#.######.########.#\n.###.#######.####.#.\n#####.##.#.##.###.##\n..#####..#.#########\n####################\n#.####....###.#.#.##\n##.#################\n#####.##.###..####..\n..######..##.#######\n####.##.####...##..#\n.#####..#.######.###\n##...#.##########...\n#.##########.#######\n.####.#.###.###.#.##\n....##.##.###..#####\n.#.#.###########.###\n#.#.#.#####.####.###\n###.##.####.##.#..##", new Point(8, 2) };
        }
    }
}