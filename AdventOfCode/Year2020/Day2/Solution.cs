using NAoCHelper;

namespace AdventOfCode.Year2020.Day2
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
        }

        public string SolvePart1()
        {
            var validCount = 0;

            foreach (var record in Input)
            {
                string[] recordParts = record.Split(':');
                var passwordPolicy = new PasswordPolicy(recordParts[0], PasswordPolicyType.Count);
                if (passwordPolicy.IsValid(recordParts[1].Trim()))
                    validCount++;
            }

            return $"Part 1: {validCount}";
        }

        public string SolvePart2()
        {
            var validCount = 0;

            foreach (var record in Input)
            {
                string[] recordParts = record.Split(':');
                var passwordPolicy = new PasswordPolicy(recordParts[0], PasswordPolicyType.Index);
                if (passwordPolicy.IsValid(recordParts[1].Trim()))
                    validCount++;
            }
            
            return $"Part 2: {validCount}";
        }
    }
}