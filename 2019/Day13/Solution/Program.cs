using System;
using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace Day13
{
    public class Program
    {
        public static void Main()
        {
            var user = new User(Helpers.GetCookie("af2ad03d-386b-4b2e-b3b5-ccb5f138b698"));
            var puzzle = new Puzzle(user, 2019, 13);
            var input = puzzle.GetInputAsync().Result;
            var memory = input.Split(',').Select(BigInteger.Parse).ToArray();

            Part1(memory);
            Part2(memory);
        }

        public static void Part1(BigInteger[] memory)
        {
            var ac = new ArcadeCabinet(memory);
            ac.InitialiseGrid();

            Console.WriteLine($"Block Tiles: {ac.BlockCount}");
        }

        public static void Part2(BigInteger[] memory)
        {
            memory[0] = 2; // Play for free ^_^

            var ac = new ArcadeCabinet(memory);
            ac.InitialiseGrid();

            while (ac.BlockCount > 0)
            {
                if (ac.BallLocation.x < ac.PaddleLocation.x)
                    ac.MoveJoyStick(Direction.Left);
                else if (ac.BallLocation.x > ac.PaddleLocation.x)
                    ac.MoveJoyStick(Direction.Right);
                else
                    ac.MoveJoyStick(Direction.Neutral);
            }

            Console.WriteLine($"You Win! Final Score: {ac.Score}");
        }
    }
}
