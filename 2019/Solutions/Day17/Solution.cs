using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace Solutions.Day17
{
	public class Solution : BaseSolution<BigInteger[]>, ISolvable
	{
		public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split(',').Select(BigInteger.Parse).ToArray())
		{
		}

		public string SolvePart1()
		{
			var icm = new IntCodeMachine.IntCodeMachine(Input);
			icm.Execute(false);

			var cameraGrid = new List<List<char>>{
				new List<char>()
			};
			var validOutputs = icm.Outputs.Where(o => o != "Halt").ToList();
			var rowCounter = 0;
			foreach (string t in validOutputs)
			{
				int outputCode = int.Parse(t);
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
			for (var i = 1; i < cameraGrid.Count - 1; i++)
			{
				if (cameraGrid[i + 1].Count > 0)
				{
					for (var j = 0; j < cameraGrid[i].Count; j++)
					{
						if (j > 0 && j < cameraGrid[i].Count - 1 && cameraGrid[i][j] == '#')
						{
							bool hasScaffoldAbove = cameraGrid[i - 1][j] == '#';
							bool hasScaffoldBelow = cameraGrid[i + 1][j] == '#';
							bool hasScaffoldLeft = cameraGrid[i][j - 1] == '#';
							bool hasScaffoldRight = cameraGrid[i][j + 1] == '#';

							if (hasScaffoldAbove && hasScaffoldBelow && hasScaffoldLeft && hasScaffoldRight)
								alignmentParametersSum += i * j;
						}
					}
				}
			}

			return $"Part 1: {alignmentParametersSum}";
		}
		
		public string SolvePart2() => throw new NotImplementedException();
	}
}