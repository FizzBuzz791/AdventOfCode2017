using System.Collections;
using NUnit.Framework;

namespace Day12Tests
{
    public class PartTests
    {
        [TestCaseSource(nameof(TestCases))]
        public void Part1Test(string initialState, string[] ruleInput, int expectedResult)
        {
            var result = Day12.Program.Part1(initialState, ruleInput);

            Assert.That(result, Is.EqualTo(expectedResult));
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