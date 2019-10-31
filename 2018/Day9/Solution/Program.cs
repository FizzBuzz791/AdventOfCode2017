using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        }

        public static void Part1(List<Player> players, int lastMarble)
        {
            var playingCircle = new PlayingCircle();
            var playerIndex = 0;

            for (var i = 1; i <= lastMarble; i++)
            {
                players[playerIndex].Score += playingCircle.PlaceMarble(i);

                if (playerIndex + 1 < players.Count)
                    playerIndex++;
                else
                    playerIndex = 0;
            }
        }
    }

    public class PlayingCircle
    {
        public List<Marble> Marbles { get; set; } = new List<Marble>();
        public Marble CurrentMarble { get; private set; }

        public PlayingCircle()
        {
            CurrentMarble = new Marble(0);
            Marbles.Add(CurrentMarble);
        }

        public int PlaceMarble(int value)
        {
            var score = 0;
            if (value % 23 == 0)
            {
                score += value;
                int seventhClockwiseIndex = (Marbles.IndexOf(CurrentMarble) - 7) % Marbles.Count;
                if (seventhClockwiseIndex < 0)
                {
                    seventhClockwiseIndex = Marbles.Count + seventhClockwiseIndex;
                }

                score += Marbles[seventhClockwiseIndex].Value;
                Marbles.RemoveAt(seventhClockwiseIndex);

                CurrentMarble = Marbles[seventhClockwiseIndex];
            }
            else
            {
                var newMarble = new Marble(value);

                if (Marbles.Count == 1)
                {
                    // Special case, always goes after, so no need to calculate index.
                    Marbles.Add(newMarble);
                }
                else
                {
                    int currentIndex = Marbles.IndexOf(CurrentMarble);
                    int secondClockwiseIndex = (currentIndex + 2) % Marbles.Count;

                    if (secondClockwiseIndex == 0)
                    {
                        Marbles.Add(newMarble);
                    }
                    else
                    {
                        Marbles.Insert(secondClockwiseIndex, newMarble);
                    }
                }

                CurrentMarble = newMarble;
            }

            return score;
        }
    }

    [DebuggerDisplay("Value: {" + nameof(Value) + "}")]
    public class Marble
    {
        public int Value { get; set; }

        public Marble(int value)
        {
            Value = value;
        }
    }

    [DebuggerDisplay("Score: {" + nameof(Score) + "}")]
    public class Player
    {
        public int Score { get; set; }
    }
}
