using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Day12;
using NUnit.Framework;

namespace Day12Tests
{
    public class PartTests
    {
        [TestCaseSource(nameof(TestCases))]
        public void Part1Test(string initialState, string[] ruleInput, int expectedResult)
        {
            // Arrange
            var pots = new Dictionary<int, Pot>();
            var count = 0;
            foreach (var pot in initialState.ToCharArray().Select(p => new Pot(p)))
            {
                pots.Add(count, pot);
                count++;
            }

            var rules = ruleInput.Select(r => new Rule(r)).ToList();

            // Act
            pots = Program.CalculateStateAtIteration(pots, rules);
            var potSum = Program.SumPotsWithPlants(pots);

            // Assert
            Assert.That(potSum, Is.EqualTo(expectedResult));
        }

        public static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData("#..#.#..##......###...###",
                    new string[] { "...## => #", "..#.. => #", ".#... => #", ".#.#. => #", ".#.## => #", ".##.. => #", ".#### => #", "#.#.# => #", "#.### => #", "##.#. => #", "##.## => #", "###.. => #", "###.# => #", "####. => #" },
                    325);
                yield return new TestCaseData("#...#####.#..##...##...#.##.#.##.###..##.##.#.#..#...###..####.#.....#..##..#.##......#####..####...",
                    new string[] { "#.#.# => #", "..### => .", "#..#. => #", ".#... => #", "..##. => #", "##.#. => #", "##..# => #", "####. => #", "...#. => #", "..#.# => #", ".#### => #", "#.### => .", "...## => .", "..#.. => .", "#...# => .", ".###. => #", ".#.## => .", ".##.. => #", "....# => .", "#..## => .", "##.## => #", "#.##. => .", "#.... => .", "##... => #", ".#.#. => .", "###.# => #", "##### => #", "#.#.. => .", "..... => .", ".##.# => .", "###.. => .", ".#..# => ." },
                    3337);
            }
        }
    }
}