using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Day12
{
    public class Program
    {
        private static readonly string InitialState = "#...#####.#..##...##...#.##.#.##.###..##.##.#.#..#...###..####.#.....#..##..#.##......#####..####...";

        private static readonly string[] RuleInput = new string[] {
            "#.#.# => #",
            "..### => .",
            "#..#. => #",
            ".#... => #",
            "..##. => #",
            "##.#. => #",
            "##..# => #",
            "####. => #",
            "...#. => #",
            "..#.# => #",
            ".#### => #",
            "#.### => .",
            "...## => .",
            "..#.. => .",
            "#...# => .",
            ".###. => #",
            ".#.## => .",
            ".##.. => #",
            "....# => .",
            "#..## => .",
            "##.## => #",
            "#.##. => .",
            "#.... => .",
            "##... => #",
            ".#.#. => .",
            "###.# => #",
            "##### => #",
            "#.#.. => .",
            "..... => .",
            ".##.# => .",
            "###.. => .",
            ".#..# => ."
        };

        public static void Main()
        {
            var potKeySum = Part1(InitialState, RuleInput);

            Console.WriteLine($"Pot Key Sum: {potKeySum}");
        }

        public static int Part1(string initialState, string[] ruleInput)
        {
            var pots = new Dictionary<int, Pot>();
            var count = 0;
            foreach (var pot in initialState.ToCharArray().Select(p => new Pot(p)))
            {
                pots.Add(count, pot);
                count++;
            }

            var rules = ruleInput.Select(r => new Rule(r)).ToList();

            for (int i = 0; i < 20; i++)
            {
                var newPots = new Dictionary<int, Pot>();
                foreach (var pot in pots)
                {
                    var newPotValue = pot.Value.State;

                    Pot potMinusTwo = GetPot(pots, newPots, pot.Key - 2);
                    Pot potMinusOne = GetPot(pots, newPots, pot.Key - 1);
                    Pot potPlusOne = GetPot(pots, newPots, pot.Key + 1);
                    Pot potPlusTwo = GetPot(pots, newPots, pot.Key + 2);

                    // Determine new value
                    Rule match = rules.FirstOrDefault(r => r.IsMatch(potMinusTwo, potMinusOne, pot.Value, potPlusOne, potPlusTwo));
                    newPots.Add(pot.Key, new Pot(match != null ? match.Result : '.'));
                }

                pots = newPots;
            }

            return SumPotsWithPlants(pots);
        }

        private static Pot GetPot(Dictionary<int, Pot> pots, Dictionary<int, Pot> newPots, int key)
        {
            Pot pot;

            if (pots.ContainsKey(key))
            {
                // Pot exists, will get added when it is the "current" pot.
                pot = pots[key];
            }
            else
            {
                if (newPots.ContainsKey(key))
                {
                    pot = newPots[key];
                }
                else
                {
                    pot = Pot.Empty;
                    newPots.Add(key, pot);
                }
            }

            return pot;
        }

        private static int SumPotsWithPlants(Dictionary<int, Pot> pots)
        {
            var potKeySum = 0;
            foreach (var pot in pots)
            {
                if (pot.Value.State == '#')
                {
                    potKeySum += pot.Key;
                }
            }
            return potKeySum;
        }
    }

    public class Rule
    {
        public string State { get; }
        public char Result { get; }

        public Rule(string input)
        {
            State = input.Substring(0, 5);
            Result = input.Last();
        }

        public bool IsMatch(Pot potMinusTwo, Pot potMinusOne, Pot pot, Pot potPlusOne, Pot potPlusTwo)
        {
            return State[0] == potMinusTwo.State &&
                State[1] == potMinusOne.State &&
                State[2] == pot.State &&
                State[3] == potPlusOne.State &&
                State[4] == potPlusTwo.State;
        }
    }

    public class Pot
    {
        public char State { get; set; }

        public Pot(char input)
        {
            State = input;
        }

        public static Pot Empty = new Pot('.');
    }
}
