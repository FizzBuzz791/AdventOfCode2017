using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day2
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n'))
        {
        }

        public string SolvePart1()
        {
            return $"Part 1: {ChecksumBoxIDs()}";
        }

        public string SolvePart2()
        {
            return $"Part 2: {FindCommonCharactersForPrototypeBoxIds()}";
        }
        
        private string FindCommonCharactersForPrototypeBoxIds()
        {
            var shortestPairA = string.Empty;
            var shortestPairB = string.Empty;
            var shortestDist = int.MaxValue;

            for (var i = 0; i < Input.Length; i++)
            {
                string stringA = Input[i];
                for (int j = i + 1; j < Input.Length; j++)
                {
                    string stringB = Input[j];
                    int dist = LevenshteinDistance(stringA, stringB);
                    if (dist < shortestDist)
                    {
                        shortestDist = dist;
                        shortestPairA = stringA;
                        shortestPairB = stringB;
                    }
                }
            }

            return shortestPairA.Where(character => shortestPairB.Contains(character))
                .Aggregate(string.Empty, (current, character) => current + character);
        }

        private static int LevenshteinDistance(string a, string b)
        {
            int aLength = a.Length;
            int bLength = b.Length;
            int[,] d = new int[aLength + 1, bLength + 1];

            if (aLength == 0)
                return bLength;

            if (bLength == 0)
                return aLength;

            for (var i = 0; i <= aLength; d[i,0] = i++){}
            for (var j = 0; j <= bLength; d[0,j] = j++){}

            for (var i = 1; i <= aLength; i++)
            {
                for (var j = 1; j <= bLength; j++)
                {
                    int cost = b[j-1] == a[i-1] ? 0 : 1;

                    d[i, j] = Math.Min(
                        Math.Min(
                            d[i - 1, j] + 1,
                            d[i, j - 1] + 1
                        ),
                        d[i - 1, j - 1] + cost
                    );
                }
            }

            return d[aLength, bLength];
        }

        private int ChecksumBoxIDs()
        {
            var doubleCount = 0;
            var tripleCount = 0;

            foreach (var boxId in Input)
            {
                var dict = new Dictionary<char, int>();
                foreach (char a in boxId)
                {
                    if (dict.ContainsKey(a))
                        dict[a]++;
                    else
                        dict.Add(a, 1);
                }

                if (dict.Any(kvp => kvp.Value == 2))
                    doubleCount++;

                if (dict.Any(kvp => kvp.Value == 3))
                    tripleCount++;
            }

            return doubleCount * tripleCount;
        }
    }
}