using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace Solutions.Day5
{
    public class Solution : BaseSolution<BigInteger[]>, ISolvable
    {
	    public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split(',').Select(BigInteger.Parse).ToArray())
	    {
	    }

	    public string SolvePart1()
	    {
		    var computer = new IntCodeMachine.IntCodeMachine(Input, new[] { 1 });
		    computer.Execute(false);

		    return $"Part 1: {computer.Outputs[^2]}";
	    }

	    public string SolvePart2()
	    {
		    var computer = new IntCodeMachine.IntCodeMachine(Input, new[] { 5 });
		    computer.Execute(false);

		    return $"Part 2: {computer.Outputs[^2]}";
	    }
    }
}