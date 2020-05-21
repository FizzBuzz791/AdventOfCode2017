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

            Part1(input);
        }

        public static void Part1(string input)
        {
            var reactions = input.Split("\n").Select(r => new Reaction(r)).ToList();

            var requiredOre = FindRequiredOre(reactions);

            Console.WriteLine($"Required ORE: {requiredOre}.");
        }

        public static int FindRequiredOre(List<Reaction> reactions)
        {
            var needs = new Dictionary<string, int> { { FUEL, 1 } };
            var extras = new Dictionary<string, int>();
            var ore = 0;

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

                    var numReactions = Math.Ceiling((double)required / produced);
                    if (!numReactions.IsWholeNumber())
                        numReactions += 1;

                    if (extras.ContainsKey(currentChemical))
                        extras[currentChemical] += ((int)numReactions * produced) - required;
                    else
                        extras.Add(currentChemical, ((int)numReactions * produced) - required);

                    foreach (var input in reaction.Inputs)
                    {
                        if (input.Name == ORE)
                        {
                            ore += input.Amount * (int)numReactions;
                        }
                        else
                        {
                            if (needs.ContainsKey(input.Name))
                                needs[input.Name] += input.Amount * (int)numReactions;
                            else
                                needs.Add(input.Name, input.Amount * (int)numReactions);
                        }

                    }
                }
            }

            return ore;
        }
    }
}
