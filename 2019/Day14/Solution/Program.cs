using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace Day14
{
    public partial class Program
    {
        private const string ORE = "ORE";
        private const string FUEL = "FUEL";

        public static void Main()
        {
            var user = new User(Helpers.GetCookie("f66a053b-e715-4168-b677-b51ee0115730"));
            var puzzle = new Puzzle(user, 2019, 14);
            var input = puzzle.GetInputAsync().Result;

            // Strip the newline
            input = input[0..^1];

            var reactions = input.Split("\n").Select(r => new Reaction(r)).ToList();

            Part1(reactions);
            Part2(reactions);
        }

        public static void Part1(List<Reaction> reactions)
        {
            var requiredOre = FindRequiredOre(reactions, 1);

            Console.WriteLine($"Required ORE: {requiredOre}.");
        }

        public static void Part2(List<Reaction> reactions)
        {
            var maxFuel = FindMaxFuel(reactions);

            Console.WriteLine($"Max FUEL: {maxFuel}.");
        }

        public static double FindRequiredOre(List<Reaction> reactions, double amountOfFuel)
        {
            var needs = new Dictionary<string, double> { { FUEL, amountOfFuel } };
            var extras = new Dictionary<string, double>();
            var ore = 0.0;

            while (needs.Count > 0)
            {
                var currentChemical = needs.Keys.First();
                if (extras.ContainsKey(currentChemical) && needs[currentChemical] <= extras[currentChemical])
                {
                    extras[currentChemical] -= needs[currentChemical];
                    needs.Remove(currentChemical);
                }
                else
                {
                    var required = extras.ContainsKey(currentChemical) ? needs[currentChemical] - extras[currentChemical] : needs[currentChemical];
                    needs.Remove(currentChemical);
                    extras.Remove(currentChemical);

                    var reaction = reactions.Single(r => r.Output.Name == currentChemical);
                    var produced = reaction.Output.Amount;

                    var numReactions = required / produced;
                    numReactions = Math.Floor(numReactions) * produced == required ? Math.Floor(numReactions) : Math.Floor(numReactions) + 1;

                    if (extras.ContainsKey(currentChemical))
                        extras[currentChemical] += (numReactions * produced) - required;
                    else
                        extras.Add(currentChemical, (numReactions * produced) - required);

                    foreach (var input in reaction.Inputs)
                    {
                        if (input.Name == ORE)
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

        public static int FindMaxFuel(List<Reaction> reactions)
        {
            const double FUEL_TARGET = 1e12;
            var minFuel = Math.Floor(FUEL_TARGET / FindRequiredOre(reactions, 1));
            var maxFuel = 10 * minFuel;

            // Calculate reasonable bounds.
            while (FindRequiredOre(reactions, maxFuel) < FUEL_TARGET)
            {
                minFuel = maxFuel;
                maxFuel = 10 * minFuel;
            }

            // Tighten the range by calculating how much fuel without going past FUEL_TARGET
            while (minFuel < maxFuel - 1)
            {
                var mid = Math.Floor((minFuel + maxFuel) / 2);
                var ore = FindRequiredOre(reactions, mid);

                if (ore < FUEL_TARGET)
                    minFuel = mid;
                else if (ore > FUEL_TARGET)
                    maxFuel = mid;
                else
                    break;
            }

            return (int)Math.Floor((minFuel + maxFuel) / 2);
        }
    }
}
