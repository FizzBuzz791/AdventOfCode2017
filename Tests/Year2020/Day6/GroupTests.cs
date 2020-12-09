using AdventOfCode.Year2020.Day6;
using NUnit.Framework;
using Shouldly;

namespace Tests.Year2020.Day6
{
    public class GroupTests
    {
        [TestCase("abc", 3)]
        [TestCase("a\nb\nc", 3)]
        [TestCase("ab\nac", 3)]
        [TestCase("a\na\na\na", 1)]
        [TestCase("b", 1)]
        [TestCase("abcx\nabcy\nabcz", 6)]
        public void UniqueAnswersReturnsExpectedResult(string group, int uniqueAnswers)
        {
            new Group { Answers = group.Split('\n') }.UniqueAnswersCount.ShouldBe(uniqueAnswers);
        }

        [TestCase("abc", 3)]
        [TestCase("a\nb\nc", 0)]
        [TestCase("ab\nac", 1)]
        [TestCase("a\na\na\na", 1)]
        [TestCase("b", 1)]
        public void AllAnswersReturnsExpectedResult(string group, int allAnswers)
        {
            new Group {Answers = group.Split('\n')}.AllAnswersCount.ShouldBe(allAnswers);
        }
    }
}