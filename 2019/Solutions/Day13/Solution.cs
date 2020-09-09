using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace Solutions.Day13
{
    public class Solution : BaseSolution<BigInteger[]>, ISolvable
    {
	    public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split(',').Select(BigInteger.Parse).ToArray())
	    {
	    }

	    public string SolvePart1()
	    {
		    var ac = new ArcadeCabinet(Input);
		    ac.InitialiseGrid();

		    return $"Part 1: {ac.BlockCount}";
	    }

	    public string SolvePart2()
	    {
		    Input[0] = 2; // Play for free ^_^
		    
		    var ac = new ArcadeCabinet(Input);
		    ac.InitialiseGrid();

		    while (ac.BlockCount > 0)
		    {
			    if (ac.BallLocation.X < ac.PaddleLocation.X)
				    ac.MoveJoyStick(Direction.Left);
			    else if (ac.BallLocation.X > ac.PaddleLocation.X)
				    ac.MoveJoyStick(Direction.Right);
			    else
				    ac.MoveJoyStick(Direction.Neutral);
		    }

		    return $"Part 2: {ac.Score}";
	    }
    }
}