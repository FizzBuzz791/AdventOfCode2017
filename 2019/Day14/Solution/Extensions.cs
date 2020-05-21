using System;

namespace Day14
{
    public static class Extensions
    {
        /// <summary>
        /// Determines if the given number is a whole number (i.e. 1.0 is whole, 1.1 is not).
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static bool IsWholeNumber(this double number)
        {
            return Math.Abs(number % 1) <= (double.Epsilon * 100);
        }
    }
}