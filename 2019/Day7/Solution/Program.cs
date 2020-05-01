using System;
using System.Collections.Generic;
using System.Linq;
using NAoCHelper;

namespace Day7
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("98fda5c2-62f1-4660-abe5-48b5d862e52f"));
            var puzzle = new Puzzle(user, 2019, 7);
            var input = puzzle.GetInputAsync().Result;
            var memory = input.Split(',').Select(Int32.Parse).ToArray();

            var maxOutput = Part1(memory);
            Console.WriteLine($"Maximum Signal: {maxOutput}");
        }

        public static int Part1(int[] memory)
        {
            // Do for all combinations of Phase
            var permutations = GetPermutations(Enumerable.Range(0, 5), 5);
            var maxOutput = Int32.MinValue;
            foreach (var permutation in permutations)
            {
                // For Amp A -> E
                var signal = 0;
                foreach (var phase in permutation)
                {
                    var amplifier = new IntCodeMachine(memory, new int[] { phase, signal });
                    amplifier.Execute(false);
                    signal = Int32.Parse(amplifier.Outputs.First());
                }

                // Record final output if higher than previous final
                if (signal > maxOutput)
                    maxOutput = signal;
            }
            return maxOutput;
        }

        public static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
                return list.Select(t => new T[] { t });

            return GetPermutations(list, length - 1).SelectMany(t => list.Where(e => !t.Contains(e)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
