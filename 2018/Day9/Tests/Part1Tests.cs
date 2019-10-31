using System.Collections;
using System.Collections.Generic;
using Day9;
using MoreLinq;
using NUnit.Framework;

namespace Day9Tests
{
    public class Tests
    {
        [TestCaseSource(nameof(TestCases))]
        public void Part1ShouldReturnExpectedScore(int playerCount, int lastMarble, int expectedResult)
        {
            // Arrange
            var players = new List<Player>();
            for (var i = 0; i < playerCount; i++)
            {
                players.Add(new Player());
            }

            // Act
            Program.Part1(players, lastMarble);

            // Assert
            Assert.That(players.MaxBy(p => p.Score).First().Score, Is.EqualTo(expectedResult));
        }

        private static IEnumerable TestCases
        {
            get
            {
                yield return new TestCaseData(9, 25, 32);
                yield return new TestCaseData(10, 1618, 8317);
                yield return new TestCaseData(13, 7999, 146373);
                yield return new TestCaseData(17, 1104, 2764);
                yield return new TestCaseData(21, 6111, 54718);
                yield return new TestCaseData(30, 5807, 37305);
                yield return new TestCaseData(405, 71700, 428690); // Actual
            }
        }
    }
}