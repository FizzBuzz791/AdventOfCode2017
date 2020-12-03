using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace AdventOfCode.Year2019.Day2
{
    public class Solution : BaseSolution<BigInteger[]>, ISolvable
    {
        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split(',').Select(BigInteger.Parse).ToArray())
        {
        }

        public string SolvePart1()
        {
            Input[1] = 12;
            Input[2] = 2;

            var computer = new IntCodeMachine.IntCodeMachine(Input);

            computer.Execute();
            return $"Part 1: {computer.Memory[0]}";
        }

        public string SolvePart2()
        {
            var foundAnswer = false;
            var answer = string.Empty;
            
            for (var noun = 0; noun < Input.Length && !foundAnswer; noun++)
            {
                for (var verb = 0; verb < Input.Length && !foundAnswer; verb++)
                {
                    Input[1] = noun;
                    Input[2] = verb;

                    var computer = new IntCodeMachine.IntCodeMachine(Input);
                    computer.Execute();

                    if (computer.Memory[0] == 19690720)
                    {
                        foundAnswer = true;
                        answer = $"Part 2: 100 * {noun} + {verb} = {100 * noun + verb}";
                    }
                }
            }

            return answer;
        }
    }
}