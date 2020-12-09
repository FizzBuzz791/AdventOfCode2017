using System;
using System.Linq;

namespace AdventOfCode.Year2020.Day6
{
    public class Group
    {
        public string[]? Answers { get; init; }

        private char[] UniqueAnswers => string.Join(string.Empty, Answers ?? Array.Empty<string>()).Distinct().ToArray();
        public int UniqueAnswersCount => UniqueAnswers.Length;

        public int AllAnswersCount
        {
            get
            {
                // Edge-case; 1 person in group
                if (Answers?.Length == 1)
                    return Answers[0].Length;
                
                string? result = null;
                var first = true;
                foreach (string answer in Answers ?? Array.Empty<string>())
                {
                    if (!first)
                    {
                        result = string.Join(string.Empty, result!.Intersect(answer));
                    }
                    else
                    {
                        first = false;
                        result = answer;
                    }
                }

                var allAnsweredCount = 0;
                foreach (char answer in result ?? string.Empty)
                {
                    if ((Answers ?? Array.Empty<string>()).All(a => a.Contains(answer)))
                        allAnsweredCount++;
                }

                return allAnsweredCount;
            }
        }
    }
}