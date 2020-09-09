using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace Solutions.Day15
{
	public class Solution : BaseSolution<BigInteger[]>, ISolvable
	{
		public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split(',').Select(BigInteger.Parse).ToArray())
		{
		}

		public string SolvePart1()
		{
			var repairDroidControlSystem = new RepairDroidControlSystem(Input);
			repairDroidControlSystem.Explore();

			int shortestPath = repairDroidControlSystem.FindPath(RepairDroidControlSystem.Origin)
			                                           .SingleOrDefault(
				                                           n => n.Location == repairDroidControlSystem.Destination)
			                                           ?.Distance ?? 0;
			return $"Part 1: {shortestPath}";
		}

		public string SolvePart2()
		{
			var repairDroidControlSystem = new RepairDroidControlSystem(Input);
			repairDroidControlSystem.Explore();

			int longestPath = repairDroidControlSystem.FindPath(repairDroidControlSystem.Destination)
			                                          .Max(x => x.Distance);
			return $"Part 2: {longestPath}";
		}
	}
}