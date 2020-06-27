using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using Common;
using NAoCHelper;

namespace Day17
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("5755d29d-585d-46d4-ba5e-5eed30d1bcca"));
            var puzzle = new Puzzle(user, 2019, 17);
            var input = puzzle.GetInputAsync().Result.Trim('\n');
            var memory = input.Split(',').Select(BigInteger.Parse).ToArray();

            Part1(memory);
        }

        public static void Part1(BigInteger[] memory)
        {
            var icm = new IntCodeMachine(memory);
            icm.Execute(false);

            var cameraGrid = new List<List<char>>{
                new List<char>()
            };
            var validOutputs = icm.Outputs.Where(o => o != "Halt").ToList();
            var rowCounter = 0;
            for (int i = 0; i < validOutputs.Count; i++)
            {
                var outputCode = int.Parse(validOutputs[i]);
                if ((char)outputCode == '\n')
                {
                    rowCounter++;
                    cameraGrid.Add(new List<char>());
                }
                else
                {
                    cameraGrid[rowCounter].Add((char)outputCode);
                }
            }

            var alignmentParametersSum = 0;
            for (int i = 1; i < cameraGrid.Count - 1; i++)
            {
                if (cameraGrid[i + 1].Count > 0)
                {
                    for (int j = 0; j < cameraGrid[i].Count(); j++)
                    {
                        if (j > 0 && j < cameraGrid[i].Count() - 1 && cameraGrid[i][j] == '#')
                        {
                            var hasScaffoldAbove = cameraGrid[i - 1][j] == '#';
                            var hasScaffoldBelow = cameraGrid[i + 1][j] == '#';
                            var hasScaffoldLeft = cameraGrid[i][j - 1] == '#';
                            var hasScaffoldRight = cameraGrid[i][j + 1] == '#';

                            if (hasScaffoldAbove && hasScaffoldBelow && hasScaffoldLeft && hasScaffoldRight)
                            {
                                alignmentParametersSum += i * j;
                            }
                        }
                    }
                }
            }

            Console.WriteLine($"Alignment Sum: {alignmentParametersSum}");
        }
    }
}
