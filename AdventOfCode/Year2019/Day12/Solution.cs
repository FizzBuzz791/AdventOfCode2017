using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Combinatorics.Collections;
using NAoCHelper;

namespace AdventOfCode.Year2019.Day12
{
    public class Solution : BaseSolution<List<Moon>>, ISolvable
    {
        private int Steps { get; set; } = 1000;
        
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split("\n").Select(i => new Moon(i)).ToList())
        {
        }

        public string SolvePart1()
        {
            List<Moon> moons = Input.Select(m => new Moon(m)).ToList();
            var combinations = new Combinations<Moon>(moons, 2);

            for (var i = 0; i < Steps; i++)
            {
                foreach (var combination in combinations)
                    ApplyGravity(combination.First(), combination.Last());

                foreach (var moon in moons)
                    moon.ApplyVelocity();
            }

            int totalEnergy = moons.Sum(m => m.PotentialEnergy * m.KineticEnergy);
            return $"Part 1: {totalEnergy}";
        }

        public string SolvePart2()
        {
            int[][] xAxis = Input.Select(m => new[] { m.Position.X, m.Velocity.X }).ToArray();
            BigInteger xAxisSteps = FindStepsToHalfway(xAxis);

            int[][] yAxis = Input.Select(m => new[] { m.Position.Y, m.Velocity.Y }).ToArray();
            BigInteger yAxisSteps = FindStepsToHalfway(yAxis);

            int[][] zAxis = Input.Select(m => new[] { m.Position.Z, m.Velocity.Z }).ToArray();
            BigInteger zAxisSteps = FindStepsToHalfway(zAxis);

            BigInteger lcm = LCM(new[] { xAxisSteps * 2, yAxisSteps * 2, zAxisSteps * 2 });
            return $"Part 2: {lcm}";
        }
        
        // Dirty, dirty hack for testing
		public void OverrideSteps(int steps)
		{
			Steps = steps;
		}

		private static void ApplyGravity(Moon first, Moon second)
		{
			if (first.Position.X < second.Position.X)
			{
				first.Velocity.X++;
				second.Velocity.X--;
			}
			else if (first.Position.X > second.Position.X)
			{
				first.Velocity.X--;
				second.Velocity.X++;
			}

			if (first.Position.Y < second.Position.Y)
			{
				first.Velocity.Y++;
				second.Velocity.Y--;
			}
			else if (first.Position.Y > second.Position.Y)
			{
				first.Velocity.Y--;
				second.Velocity.Y++;
			}

			if (first.Position.Z < second.Position.Z)
			{
				first.Velocity.Z++;
				second.Velocity.Z--;
			}
			else if (first.Position.Z > second.Position.Z)
			{
				first.Velocity.Z--;
				second.Velocity.Z++;
			}
		}

		private static void ApplyGravity(IList<int> first, IList<int> second)
		{
			if (first[0] < second[0])
			{
				first[1]++;
				second[1]--;
			}
			else if (first[0] > second[0])
			{
				first[1]--;
				second[1]++;
			}
		}

		private static BigInteger FindStepsToHalfway(IList<int[]> axis)
		{
			int[][] original = axis.Select(x => new[] { x[0], x[1] }).ToArray();
			var combinations = new Combinations<int[]>(axis, 2);
			BigInteger steps = BigInteger.Zero;

			do
			{
				foreach (var combination in combinations)
					ApplyGravity(combination.First(), combination.Last());

				foreach (var x in axis)
					ApplyVelocity(x);

				steps++;
			} while (axis[0][1] != original[0][1] || axis[1][1] != original[1][1] || axis[2][1] != original[2][1] ||
			         axis[3][1] != original[3][1]);

			return steps;
		}

		private static void ApplyVelocity(IList<int> axis)
		{
			axis[0] += axis[1];
		}

		private static BigInteger LCM(IList<BigInteger> array)
		{
			BigInteger lcm = BigInteger.One;
			var divisor = 2;

			while (true)
			{
				var counter = 0;
				var divisible = false;
				for (var i = 0; i < array.Count; i++)
				{
					if (array[i] == BigInteger.Zero)
						return BigInteger.Zero;
					
					if (array[i] < BigInteger.Zero)
						array[i] *= BigInteger.MinusOne;

					if (array[i] == BigInteger.One)
						counter++;

					if (array[i] % divisor == BigInteger.Zero)
					{
						divisible = true;
						array[i] /= divisor;
					}
				}

				if (divisible)
					lcm *= divisor;
				else
					divisor++;

				if (counter == array.Count)
					return lcm;
			}
		}
    }
}