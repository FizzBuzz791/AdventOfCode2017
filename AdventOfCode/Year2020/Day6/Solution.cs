using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2020.Day6
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
        }

        public string SolvePart1()
        {
            List<Group> groups = new();
            
            string currentGroup = string.Empty;
            foreach (string line in Input)
            {
                if (line.Length > 0)
                {
                    currentGroup += $" {line}";
                }
                else
                {
                    groups.Add(new Group { Answers = currentGroup.Split(' ') });
                    currentGroup = string.Empty;
                }
            }
            
            // Edge-case: last line is a group
            if (currentGroup.Length > 0)
                groups.Add(new Group { Answers = currentGroup.Split(' ') });

            return $"Part 1: {groups.Sum(g => g.UniqueAnswersCount)}";
        }

        public string SolvePart2()
        {
            List<Group> groups = new();
            
            string currentGroup = string.Empty;
            foreach (string line in Input)
            {
                if (line.Length > 0)
                {
                    currentGroup += string.IsNullOrWhiteSpace(currentGroup) ? line : $" {line}";
                }
                else
                {
                    groups.Add(new Group { Answers = currentGroup.Split(' ') });
                    currentGroup = string.Empty;
                }
            }
            
            // Edge-case: last line is a group
            if (currentGroup.Length > 0)
                groups.Add(new Group { Answers = currentGroup.Split(' ') });

            // 3351 too low
            return $"Part 2: {groups.Sum(g => g.AllAnswersCount)}";
        }
    }
}