using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2017.Day4
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
        }

        public string SolvePart1()
        {
            return $"Part 1: {Input.Select(passPhrase => passPhrase.Split(' ')).Count(tokens => tokens.Distinct().Count() == tokens.Length)}";
        }

        public string SolvePart2()
        {
            var validCount = 0;

            foreach (string passPhrase in Input)
            {
                var isPassPhraseValid = true;
                string[] tokens = passPhrase.Split(' ');
                for (var i = 0; i < tokens.Length; i++)
                {
                    for (int j = i + 1; j < tokens.Length; j++)
                    {
                        if (IsAnagram(tokens[i], tokens[j]))
                            isPassPhraseValid = false;
                    }
                }

                if (isPassPhraseValid)
                    validCount++;
            }

            return $"Part 2: {validCount}";
        }
        
        private static bool IsAnagram(string a, string b) => a.OrderBy(c => c).SequenceEqual(b.OrderBy(c => c));
    }
}