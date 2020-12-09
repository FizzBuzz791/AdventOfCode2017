using System.Collections.Generic;
using System.Linq;
using MoreLinq.Extensions;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day3
{
    public class Solution : BaseSolution<List<Claim>>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split('\n').Select(y => new Claim(y)).ToList())
        {
        }

        public string SolvePart1()
        {
            Claim? claimWithLargestX = Input.MaxBy(c => c.ClaimArea.X).First();
            Claim? claimWithLargestY = Input.MaxBy(c => c.ClaimArea.Y).First();

            int maxX = claimWithLargestX.ClaimArea.X + claimWithLargestX.ClaimArea.Width;
            int maxY = claimWithLargestY.ClaimArea.Y + claimWithLargestY.ClaimArea.Height;

            int[,] fabric = new int[maxX + 1, maxY + 1];

            foreach (var claim in Input)
            {
                for (int x = claim.ClaimArea.X; x < claim.ClaimArea.X + claim.ClaimArea.Width; x++)
                {
                    for (int y = claim.ClaimArea.Y; y < claim.ClaimArea.Y + claim.ClaimArea.Height; y++)
                    {
                        fabric[x, y] += 1;
                    }
                }
            }

            var sharedClaims = 0;
            for (var i = 0; i <= maxX; i++)
            {
                for (var j = 0; j <= maxY; j++)
                {
                    int targetCell = fabric[i,j];

                    if (targetCell > 1)
                        sharedClaims++;
                }
            }

            return $"Part 1: {sharedClaims}";
        }

        public string SolvePart2()
        {
            Claim? claimWithLargestX = Input.MaxBy(c => c.ClaimArea.X).First();
            Claim? claimWithLargestY = Input.MaxBy(c => c.ClaimArea.Y).First();

            int maxX = claimWithLargestX.ClaimArea.X + claimWithLargestX.ClaimArea.Width;
            int maxY = claimWithLargestY.ClaimArea.Y + claimWithLargestY.ClaimArea.Height;

            int[,] fabric = new int[maxX + 1, maxY + 1];

            List<int> overlapsWith = new();
            foreach (var claim in Input)
            {
                for (int x = claim.ClaimArea.X; x < claim.ClaimArea.X + claim.ClaimArea.Width; x++)
                {
                    for (int y = claim.ClaimArea.Y; y < claim.ClaimArea.Y + claim.ClaimArea.Height; y++)
                    {
                        if (fabric[x,y] == 0)
                            fabric[x,y] = claim.Id;
                        else
                        {
                            overlapsWith.Add(fabric[x,y]);
                            overlapsWith.Add(claim.Id); // Horribly inefficient, but I'm falling behind, so this'll do.
                            fabric[x, y] = 'X';
                        }
                    }
                }
            }

            List<Claim> noOverlaps = Input.Where(c => !overlapsWith.Contains(c.Id)).ToList();

            return $"Part 2: {noOverlaps.First().Id}";
        }
    }
}