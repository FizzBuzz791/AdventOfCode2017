using NAoCHelper;

namespace Day8
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var user = new User(Helpers.GetCookie("f4e71f6f-ce28-49d3-8515-f4e51a2ddfd7"));
            var puzzle = new Puzzle(user, 2019, 8);
            var input = puzzle.GetInputAsync().Result;
        }
    }
}
