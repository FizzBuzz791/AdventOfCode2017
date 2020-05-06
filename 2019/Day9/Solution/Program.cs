using System.Linq;
using System.Numerics;
using NAoCHelper;

namespace Day9
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("fe89d886-33f1-4478-8a4e-bc48a031be8c"));
            var puzzle = new Puzzle(user, 2019, 9);
            var input = puzzle.GetInputAsync().Result;
            var memory = input.Split(',').Select(BigInteger.Parse).ToArray();

            Part1(memory);
            Part2(memory);
        }

        public static void Part1(BigInteger[] memory)
        {
            var computer = new IntCodeMachine(memory, new int[] { 1 });
            computer.Execute(true);
        }

        public static void Part2(BigInteger[] memory)
        {
            var computer = new IntCodeMachine(memory, new int[] { 2 });
            computer.Execute(true);
        }
    }
}
