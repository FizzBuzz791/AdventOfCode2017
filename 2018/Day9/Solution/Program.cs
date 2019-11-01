using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Humanizer;
using MoreLinq;

namespace Day9
{
    public class Program
    {
        private const string Input = "405 players; last marble is worth 71700 points";

        public static void Main()
        {
            var inputParts = Input.Split(" ");
            int playerCount = int.Parse(inputParts[0]);
            int lastMarble = int.Parse(inputParts[6]);

            var players = new List<Player>();
            for (var i = 0; i < playerCount; i++)
            {
                players.Add(new Player());
            }

            Part1(players, lastMarble);
            Console.WriteLine($"Completed Part 1: {players.MaxBy(p => p.Score).First().Score}");

            players.ForEach(p => p.Score = 0);
            Part1(players, lastMarble * 100);
            Console.WriteLine($"Completed Part 2: {players.MaxBy(p => p.Score).First().Score}");
        }

        public static void Part1(List<Player> players, int lastMarble)
        {
            var playingCircle = new PlayingCircle(lastMarble + 1);
            var playerIndex = 0;

            var historicalTimes = new List<long>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (var i = 1; i <= lastMarble; i++)
            {
                players[playerIndex].Score += playingCircle.PlaceMarble(i);
                if (i % 100000 == 0)
                {
                    sw.Stop();
                    Console.WriteLine($"Placing 100000 marbles took {sw.ElapsedMilliseconds}ms");
                    Console.WriteLine($"Placed {i} marbles.");
                    historicalTimes.Add(sw.ElapsedMilliseconds);
                    CalculateRemainingTime(historicalTimes, i, lastMarble);
                    sw.Restart();
                }

                if (playerIndex + 1 < players.Count)
                    playerIndex++;
                else
                    playerIndex = 0;
            }
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

    public class PlayingCircle
    {
        public List<int> Marbles { get; set; }
        public int CurrentMarble { get; private set; }

        public PlayingCircle(int capacity)
        {
            Marbles = new List<int>(capacity);
            CurrentMarble = 0;
            Marbles.Add(CurrentMarble);
        }

        public long PlaceMarble(int value)
        {
            long score = 0;

            if (value % 23 == 0)
            {
                score = CalculateScore(value);
            }
            else
            {
                InsertMarble(value);
            }

            return score;
        }

        private long CalculateScore(int value)
        {
            long score = value;

            int seventhClockwiseIndex = (Marbles.IndexOf(CurrentMarble) - 7) % Marbles.Count;
            if (seventhClockwiseIndex < 0)
            {
                seventhClockwiseIndex = Marbles.Count + seventhClockwiseIndex;
            }

            score += Marbles[seventhClockwiseIndex];
            Marbles.RemoveAt(seventhClockwiseIndex);

            CurrentMarble = Marbles[seventhClockwiseIndex];

            return score;
        }

        private void InsertMarble(int value)
        {
            int currentIndex = Marbles.IndexOf(CurrentMarble);
            int secondClockwiseIndex = (currentIndex + 2) % Marbles.Count;

            if (secondClockwiseIndex == 0)
            {
                Marbles.Add(value);
            }
            else
            {
                Marbles.Insert(secondClockwiseIndex, value);
            }

            CurrentMarble = value;
        }
    }

    [DebuggerDisplay("Score: {" + nameof(Score) + "}")]
    public class Player
    {
        public long Score { get; set; }
    }
}
