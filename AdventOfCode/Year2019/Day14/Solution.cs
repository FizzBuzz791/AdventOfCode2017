using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace AdventOfCode.Year2019.Day14
{
    public class Solution : BaseSolution<List<Reaction>>, ISolvable
    {
	    private const string Ore = "ORE";
	    private const string Fuel = "FUEL";
	    
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Trim('\n').Split("\n").Select(y => new Reaction(y)).ToList())
        {
        }

		public string SolvePart1()
		{
			double requiredOre = FindRequiredOre(Input, 1);

			return $"Part 1: {requiredOre}";
		}

		public string SolvePart2()
		{
			int maxFuel = FindMaxFuel(Input);

			return $"Part 2: {maxFuel}";
		}

		private static double FindRequiredOre(IReadOnlyCollection<Reaction> reactions, double amountOfFuel)
		{
			var needs = new Dictionary<string, double> { { Fuel, amountOfFuel } };
			var extras = new Dictionary<string, double>();
			var ore = 0.0;

			while (needs.Count > 0)
			{
				string currentChemical = needs.Keys.First();
				if (extras.ContainsKey(currentChemical) && needs[currentChemical] <= extras[currentChemical])
				{
					extras[currentChemical] -= needs[currentChemical];
					needs.Remove(currentChemical);
				}
				else
				{
					double required = extras.ContainsKey(currentChemical)
						? needs[currentChemical] - extras[currentChemical]
						: needs[currentChemical];
					needs.Remove(currentChemical);
					extras.Remove(currentChemical);

					Reaction reaction = reactions.Single(r => r.Output.Name == currentChemical);
					double produced = reaction.Output.Amount;

					double numReactions = required / produced;
					numReactions = Math.Abs(Math.Floor(numReactions) * produced - required) < double.Epsilon
						? Math.Floor(numReactions)
						: Math.Floor(numReactions) + 1;

					if (extras.ContainsKey(currentChemical))
						extras[currentChemical] += numReactions * produced - required;
					else
						extras.Add(currentChemical, numReactions * produced - required);

					foreach (var input in reaction.Inputs)
					{
						if (input.Name == Ore)
						{
							ore += input.Amount * numReactions;
						}
						else
						{
							if (needs.ContainsKey(input.Name))
								needs[input.Name] += input.Amount * numReactions;
							else
								needs.Add(input.Name, input.Amount * numReactions);
						}
					}
				}
			}

			return ore;
		}

		private static int FindMaxFuel(IReadOnlyCollection<Reaction> reactions)
		{
			const double fuelTarget = 1e12;
			double minFuel = Math.Floor(fuelTarget / FindRequiredOre(reactions, 1));
			double maxFuel = 10 * minFuel;

			// Calculate reasonable bounds.
			while (FindRequiredOre(reactions, maxFuel) < fuelTarget)
			{
				minFuel = maxFuel;
				maxFuel = 10 * minFuel;
			}

			// Tighten the range by calculating how much fuel without going past FUEL_TARGET
			while (minFuel < maxFuel - 1)
			{
				double mid = Math.Floor((minFuel + maxFuel) / 2);
				double ore = FindRequiredOre(reactions, mid);

				if (ore < fuelTarget)
					minFuel = mid;
				else if (ore > fuelTarget)
					maxFuel = mid;
				else
					break;
			}

			return (int) Math.Floor((minFuel + maxFuel) / 2);
		}
    }
}