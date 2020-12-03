using NAoCHelper;

namespace AdventOfCode.Year2017.Day1
{
    public class Solution : BaseSolution<char[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.ToCharArray())
        {
        }

        public string SolvePart1() => $"Part 1: {SolveCaptcha(Input, 1)}";

        public string SolvePart2() => $"Part 2: {SolveCaptcha(Input, Input.Length / 2)}";
        
        private static int SolveCaptcha(char[] tokens, int step)
        {
            var sum = 0;
            for (var i = 0; i < tokens.Length; i++)
            {
                int nextNumberIndex = i + step;
                if (nextNumberIndex >= tokens.Length)
                    nextNumberIndex -= tokens.Length;

                if (tokens[i] == tokens[nextNumberIndex])
                    sum += int.Parse(tokens[i].ToString());
            }
            return sum;
        }
    }
}