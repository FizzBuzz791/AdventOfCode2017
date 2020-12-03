using System;
using AdventOfCode;
using NAoCHelper;

var user = new User(Helpers.GetCookie("2c64fe11-6fc3-4dcc-8d5a-a3af6c90b8a4"));

int year = int.Parse(args[0]);
int day = int.Parse(args[1]);

var puzzle = new Puzzle(user, year, day);
ISolvable? solution = year switch
{
    2017 => day switch
    {
        1 => new AdventOfCode.Year2017.Day1.Solution(puzzle),
        2 => new AdventOfCode.Year2017.Day2.Solution(puzzle),
        3 => new AdventOfCode.Year2017.Day3.Solution(puzzle),
        _ => null
    },
    2018 => day switch
    {
        1 => new AdventOfCode.Year2018.Day1.Solution(puzzle),
        2 => new AdventOfCode.Year2018.Day2.Solution(puzzle),
        3 => new AdventOfCode.Year2018.Day3.Solution(puzzle),
        4 => new AdventOfCode.Year2018.Day4.Solution(puzzle),
        5 => new AdventOfCode.Year2018.Day5.Solution(puzzle),
        6 => new AdventOfCode.Year2018.Day6.Solution(puzzle),
        7 => new AdventOfCode.Year2018.Day7.Solution(puzzle),
        8 => new AdventOfCode.Year2018.Day8.Solution(puzzle),
        9 => new AdventOfCode.Year2018.Day9.Solution(puzzle),
        10 => new AdventOfCode.Year2018.Day10.Solution(puzzle),
        11 => new AdventOfCode.Year2018.Day11.Solution(puzzle),
        _ => null
    },
    2019 => day switch
    {
      1 => new AdventOfCode.Year2019.Day1.Solution(puzzle),
      2 => new AdventOfCode.Year2019.Day2.Solution(puzzle),
      3 => new AdventOfCode.Year2019.Day3.Solution(puzzle),
      4 => new AdventOfCode.Year2019.Day4.Solution(puzzle),
      5 => new AdventOfCode.Year2019.Day5.Solution(puzzle),
      6 => new AdventOfCode.Year2019.Day6.Solution(puzzle),
      7 => new AdventOfCode.Year2019.Day7.Solution(puzzle),
      8 => new AdventOfCode.Year2019.Day8.Solution(puzzle),
      9 => new AdventOfCode.Year2019.Day9.Solution(puzzle),
      10 => new AdventOfCode.Year2019.Day10.Solution(puzzle),
      11 => new AdventOfCode.Year2019.Day11.Solution(puzzle),
      12 => new AdventOfCode.Year2019.Day12.Solution(puzzle),
      13 => new AdventOfCode.Year2019.Day13.Solution(puzzle),
      14 => new AdventOfCode.Year2019.Day14.Solution(puzzle),
      15 => new AdventOfCode.Year2019.Day15.Solution(puzzle),
      16 => new AdventOfCode.Year2019.Day16.Solution(puzzle),
      _ => null
    },
    2020 => day switch
    {
        1 => new AdventOfCode.Year2020.Day1.Solution(puzzle),
        2 => new AdventOfCode.Year2020.Day2.Solution(puzzle),
        3 => new AdventOfCode.Year2020.Day3.Solution(puzzle),
        _ => null
    },
    _ => null
};

if (solution != null)
{
    Console.WriteLine(solution.SolvePart1());
    Console.WriteLine(solution.SolvePart2());
}
else
{
    Console.WriteLine($"No solution for {year}, Day {day}");
}