using System;
using NAoCHelper;

namespace AdventOfCode
{
    public abstract class BaseSolution<T>
    {
        protected readonly T Input;

        protected BaseSolution(IPuzzle puzzle, Func<string, T> inputSelector) =>
            Input = inputSelector(puzzle.GetInputAsync().Result);
    }
}