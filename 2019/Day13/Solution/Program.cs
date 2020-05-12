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
        }

        public static void Part1(BigInteger[] memory)
        {
            var ac = new ArcadeCabinet(memory);
            ac.GenerateGrid();

            Console.WriteLine($"Block Tiles: {ac.BlockTileCount}");
        }
    }
}
