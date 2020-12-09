using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Humanizer;
using MoreLinq;
using MoreLinq.Extensions;
using NAoCHelper;

namespace AdventOfCode.Year2018.Day9
{
    public class Solution : BaseSolution<string[]>, ISolvable
    {
        private readonly int _lastMarble;
        private readonly List<Player> _players = new();

        public Solution(IPuzzle puzzle) : base(puzzle, x => x.Split(' '))
        {
            int playerCount = int.Parse(Input[0]);
            _lastMarble = int.Parse(Input[6]);
            
            for (var i = 0; i < playerCount; i++)
            {
                _players.Add(new Player());
            }
        }

        public string SolvePart1()
        {
            var playingCircle = new PlayingCircle(_lastMarble + 1);
            var playerIndex = 0;

            var historicalTimes = new List<long>();
            Stopwatch sw = new();
            sw.Start();
            for (var i = 1; i <= _lastMarble; i++)
            {
                _players[playerIndex].Score += playingCircle.PlaceMarble(i);
                if (i % 100000 == 0)
                {
                    sw.Stop();
                    Console.WriteLine($"Placing 100000 marbles took {sw.ElapsedMilliseconds}ms");
                    Console.WriteLine($"Placed {i} marbles.");
                    historicalTimes.Add(sw.ElapsedMilliseconds);
                    CalculateRemainingTime(historicalTimes, i, _lastMarble);
                    sw.Restart();
                }

                playerIndex = playerIndex + 1 < _players.Count ? playerIndex + 1 : 0;
            }

            return $"Part 1: {FirstExtension.First(MoreEnumerable.MaxBy(_players, p => p.Score)).Score}";
        }

        public string SolvePart2()
        {
            // Reset state
            _players.ForEach(p => p.Score = 0);
            int lastMarble = _lastMarble * 100;
            
            var playingCircle = new PlayingCircle(lastMarble + 1);
            var playerIndex = 0;

            var historicalTimes = new List<long>();
            Stopwatch sw = new();
            sw.Start();
            for (var i = 1; i <= lastMarble; i++)
            {
                _players[playerIndex].Score += playingCircle.PlaceMarble(i);
                if (i % 100000 == 0)
                {
                    sw.Stop();
                    Console.WriteLine($"Placing 100000 marbles took {sw.ElapsedMilliseconds}ms");
                    Console.WriteLine($"Placed {i} marbles.");
                    historicalTimes.Add(sw.ElapsedMilliseconds);
                    CalculateRemainingTime(historicalTimes, i, lastMarble);
                    sw.Restart();
                }

                playerIndex = playerIndex + 1 < _players.Count ? playerIndex + 1 : 0;
            }

            return $"Part 2: {FirstExtension.First(MoreEnumerable.MaxBy(_players, p => p.Score)).Score}";
        }
        
        private static void CalculateRemainingTime(IEnumerable<long> historicalTimes, int currentMarble, int totalMarbles)
        {
            double averageTime = MoreEnumerable.TakeLast(historicalTimes, 5).Average();
            int remainingMarbles = totalMarbles - currentMarble;
            int remainingIncrements = remainingMarbles / 100000;
            double remainingTime = remainingIncrements * averageTime;
            Console.WriteLine($"Estimated time remaining: {TimeSpan.FromMilliseconds(remainingTime).Humanize(4)}");
        }
    }
}