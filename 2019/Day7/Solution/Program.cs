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

            maxOutput = Part2(memory);
            Console.WriteLine($"Maximum Signal w/Feedback Loop: {maxOutput}");
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

        public static int Part2(int[] memory)
        {
            var permutations = GetPermutations(Enumerable.Range(5, 5), 5);
            var maxOutput = Int32.MinValue;
            foreach (var permutation in permutations)
            {
                var ampA = new IntCodeMachine(memory, new int[] { permutation.ToArray()[0] });
                var ampAOutput = string.Empty;
                var ampB = new IntCodeMachine(memory, new int[] { permutation.ToArray()[1] });
                var ampBOutput = string.Empty;
                var ampC = new IntCodeMachine(memory, new int[] { permutation.ToArray()[2] });
                var ampCOutput = string.Empty;
                var ampD = new IntCodeMachine(memory, new int[] { permutation.ToArray()[3] });
                var ampDOutput = string.Empty;
                var ampE = new IntCodeMachine(memory, new int[] { permutation.ToArray()[4] });
                var ampEOutput = string.Empty;

                var signal = 0;

                do
                {
                    ampA.InputValues.Enqueue(signal);
                    ampA.Execute(false);
                    ampAOutput = ampA.Outputs.Last();
                    signal = Int32.Parse(ampAOutput == "Halt" ? ampA.Outputs[ampA.Outputs.Count - 2] : ampAOutput);

                    ampB.InputValues.Enqueue(signal);
                    ampB.Execute(false);
                    ampBOutput = ampB.Outputs.Last();
                    signal = Int32.Parse(ampBOutput == "Halt" ? ampB.Outputs[ampB.Outputs.Count - 2] : ampBOutput);

                    ampC.InputValues.Enqueue(signal);
                    ampC.Execute(false);
                    ampCOutput = ampC.Outputs.Last();
                    signal = Int32.Parse(ampCOutput == "Halt" ? ampC.Outputs[ampC.Outputs.Count - 2] : ampCOutput);

                    ampD.InputValues.Enqueue(signal);
                    ampD.Execute(false);
                    ampDOutput = ampD.Outputs.Last();
                    signal = Int32.Parse(ampDOutput == "Halt" ? ampD.Outputs[ampD.Outputs.Count - 2] : ampDOutput);

                    ampE.InputValues.Enqueue(signal);
                    ampE.Execute(false);
                    ampEOutput = ampE.Outputs.Last();
                    signal = Int32.Parse(ampEOutput == "Halt" ? ampE.Outputs[ampE.Outputs.Count - 2] : ampEOutput);
                } while (ampEOutput != "Halt");

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
