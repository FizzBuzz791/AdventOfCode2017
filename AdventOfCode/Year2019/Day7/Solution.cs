using System.Linq;
using System.Numerics;
using Combinatorics.Collections;
using NAoCHelper;

namespace AdventOfCode.Year2019.Day7
{
    public class Solution : BaseSolution<BigInteger[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split(',').Select(BigInteger.Parse).ToArray())
        {
        }

        public string SolvePart1()
        {
            // Do for all combinations of Phase
            var permutations = new Permutations<int>(Enumerable.Range(0, 5).ToList());
            var maxOutput = int.MinValue;
            foreach (var permutation in permutations)
            {
                // For Amp A -> E
                var signal = 0;
                foreach (int phase in permutation)
                {
                    var amplifier = new IntCodeMachine.IntCodeMachine(Input, new[] { phase, signal });
                    amplifier.Execute(false);
                    signal = int.Parse(amplifier.Outputs.First());
                }

                // Record final output if higher than previous final
                if (signal > maxOutput)
                    maxOutput = signal;
            }

            return $"Part 1: {maxOutput}";
        }

        public string SolvePart2()
        {
            var permutations = new Permutations<int>(Enumerable.Range(5, 5).ToList());
            var maxOutput = int.MinValue;
            foreach (var permutation in permutations)
            {
                var ampA = new IntCodeMachine.IntCodeMachine(Input, new[] { permutation.ToArray()[0] });
                var ampB = new IntCodeMachine.IntCodeMachine(Input, new[] { permutation.ToArray()[1] });
                var ampC = new IntCodeMachine.IntCodeMachine(Input, new[] { permutation.ToArray()[2] });
                var ampD = new IntCodeMachine.IntCodeMachine(Input, new[] { permutation.ToArray()[3] });
                var ampE = new IntCodeMachine.IntCodeMachine(Input, new[] { permutation.ToArray()[4] });

                var signal = 0;
                string ampEOutput;
                do
                {
                    ampA.InputValues.Enqueue(signal);
                    ampA.Execute(false);
                    string ampAOutput = ampA.Outputs.Last();
                    signal = int.Parse(ampAOutput == "Halt" ? ampA.Outputs[^2] : ampAOutput);

                    ampB.InputValues.Enqueue(signal);
                    ampB.Execute(false);
                    string ampBOutput = ampB.Outputs.Last();
                    signal = int.Parse(ampBOutput == "Halt" ? ampB.Outputs[^2] : ampBOutput);

                    ampC.InputValues.Enqueue(signal);
                    ampC.Execute(false);
                    string ampCOutput = ampC.Outputs.Last();
                    signal = int.Parse(ampCOutput == "Halt" ? ampC.Outputs[^2] : ampCOutput);

                    ampD.InputValues.Enqueue(signal);
                    ampD.Execute(false);
                    string ampDOutput = ampD.Outputs.Last();
                    signal = int.Parse(ampDOutput == "Halt" ? ampD.Outputs[^2] : ampDOutput);

                    ampE.InputValues.Enqueue(signal);
                    ampE.Execute(false);
                    ampEOutput = ampE.Outputs.Last();
                    signal = int.Parse(ampEOutput == "Halt" ? ampE.Outputs[^2] : ampEOutput);
                } while (ampEOutput != "Halt");

                if (signal > maxOutput)
                    maxOutput = signal;
            }

            return $"Part 2: {maxOutput}";
        }
    }
}