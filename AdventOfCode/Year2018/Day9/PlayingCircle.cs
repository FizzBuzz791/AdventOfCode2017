using System.Collections.Generic;

namespace AdventOfCode.Year2018.Day9
{
    public class PlayingCircle
    {
        private List<int> Marbles { get; }
        private int CurrentMarble { get; set; }

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
                score = CalculateScore(value);
            else
                InsertMarble(value);

            return score;
        }

        private long CalculateScore(int value)
        {
            long score = value;

            int seventhClockwiseIndex = (Marbles.IndexOf(CurrentMarble) - 7) % Marbles.Count;
            if (seventhClockwiseIndex < 0)
                seventhClockwiseIndex = Marbles.Count + seventhClockwiseIndex;

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
                Marbles.Add(value);
            else
                Marbles.Insert(secondClockwiseIndex, value);

            CurrentMarble = value;
        }
    }
}