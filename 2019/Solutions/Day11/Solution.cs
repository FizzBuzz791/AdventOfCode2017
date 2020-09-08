using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using NAoCHelper;

namespace Solutions.Day11
{
	public class Solution : BaseSolution<BigInteger[]>, ISolvable
	{
		public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split(',').Select(BigInteger.Parse).ToArray())
		{
		}

		public string SolvePart1()
		{
			var robot = new Robot(Input);
			robot.Run();
			return $"Part 1: {robot.PaintedPanels.Count}";
		}

		public string SolvePart2()
		{
			var robot = new Robot(Input);
			robot.Run(Color.White);

			int minX = robot.PaintedPanels.Keys.Min(p => p.X);
			int minY = robot.PaintedPanels.Keys.Min(p => p.Y);
			int maxX = robot.PaintedPanels.Keys.Max(p => p.X);
			int maxY = robot.PaintedPanels.Keys.Max(p => p.Y);

			var output = new StringBuilder();
			for (int y = minY; y <= maxY; y++)
			{
				for (int x = minX; x <= maxX; x++)
				{
					var currentPoint = new Point(x, y);
					if (robot.PaintedPanels.ContainsKey(currentPoint))
						output.Append(robot.PaintedPanels[currentPoint] == Color.White ? '#' : ' ');
					else
						output.Append(' '); // Black
				}

				output.AppendLine();
			}

			return $"Part 2:\n{output}";
		}
	}
}