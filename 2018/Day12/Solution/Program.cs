using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Humanizer;

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
            var pots = new Dictionary<int, Pot>();
            var count = 0;
            foreach (var pot in InitialState.ToCharArray().Select(p => new Pot(p)))
            {
                pots.Add(count, pot);
                count++;
            }

            var rules = RuleInput.Select(r => new Rule(r)).ToList();
            // Micro-optimisation
            rules = rules.Where(r => r.Result == '#').ToList();

            var timer = new Stopwatch();
            timer.Start();
            var part1Pots = CalculateStateAtIteration(pots, rules);
            var part1Sum = SumPotsWithPlants(part1Pots);
            timer.Stop();
            Console.WriteLine($"(1) Calculated Pot Key Sum: {part1Sum} for {20} iterations in {TimeSpan.FromMilliseconds(timer.ElapsedMilliseconds).Humanize(4)}");

            timer.Restart();
            var part2Pots = CalculateStateAtIteration(pots, rules, 50000000000L);
            var part2Sum = SumPotsWithPlants(part2Pots);
            timer.Stop();
            Console.WriteLine($"(2) Calculated Pot Key Sum: {part2Sum} for {50000000000} iterations in {TimeSpan.FromMilliseconds(timer.ElapsedMilliseconds).Humanize(4)}");
        }

        public static Dictionary<int, Pot> CalculateStateAtIteration(Dictionary<int, Pot> pots, List<Rule> rules, long iterations = 20)
        {
            var newPots = new Dictionary<int, Pot>();
            for (int i = 0; i < iterations; i++)
            {
                foreach (var pot in pots)
                {
                    Pot potMinusTwo = GetPot(pots, newPots, pot.Key - 2);
                    Pot potMinusOne = GetPot(pots, newPots, pot.Key - 1);
                    Pot potPlusOne = GetPot(pots, newPots, pot.Key + 1);
                    Pot potPlusTwo = GetPot(pots, newPots, pot.Key + 2);

                    // Determine new value
                    Rule match = rules.FirstOrDefault(r => r.IsMatch(potMinusTwo, potMinusOne, pot.Value, potPlusOne, potPlusTwo));
                    Pot result = match != null ? new Pot(match.Result) : Pot.Empty;
                    newPots.Add(pot.Key, match != null ? new Pot(match.Result) : Pot.Empty);
                }

                // Optimise the pots list so it doesn't grow out of control and slow down the algorithm.
                int minKey = newPots.Keys.Min();
                while (newPots[minKey].State == '.' && newPots[minKey + 1].State == '.' && newPots[minKey + 2].State == '.' && newPots[minKey + 3].State == '.' && newPots[minKey + 4].State == '.')
                {
                    newPots.Remove(minKey);
                    minKey = newPots.Keys.Min();
                }

                int maxKey = newPots.Keys.Max();
                while (newPots[maxKey].State == '.' && newPots[maxKey - 1].State == '.' && newPots[maxKey - 2].State == '.' && newPots[maxKey - 3].State == '.' && newPots[maxKey - 4].State == '.')
                {
                    newPots.Remove(maxKey);
                    maxKey = newPots.Keys.Max();
                }
                pots = newPots.ToDictionary(p => p.Key, p => p.Value);
                newPots.Clear();

                if (i % 1000000 == 0)
                {
                    Console.WriteLine($"Completed {i} iterations");
                }
            }

            return pots;
        }

        public static Pot GetPot(Dictionary<int, Pot> pots, Dictionary<int, Pot> newPots, int key)
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

        public static long SumPotsWithPlants(Dictionary<int, Pot> pots)
        {
            var potKeySum = 0L;
            foreach (var pot in pots.Where(p => p.Value.State == '#'))
            {
                if (potKeySum + pot.Key >= Int64.MaxValue)
                {
                    Console.Error.WriteLine("Integer 64 Overflow");
                }
                potKeySum += pot.Key;
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
