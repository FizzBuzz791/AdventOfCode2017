using System.Collections.Generic;
using NUnit.Framework;
using Extensions;
using System.Linq;
using Shouldly;

namespace ExtensionsTests
{
    public class IEnumerableExtensionsTests
    {
        [TestCaseSource(nameof(PermutationCases))]
        public void GeneratesAllPossiblePermutations(IEnumerable<int> list, int length, IEnumerable<IEnumerable<int>> expectedResult)
        {
            // Arrange

            // Act
            var result = list.GetPermutations(length);

            // Assert
            result.ShouldBe(expectedResult);
        }

        private static IEnumerable<object[]> PermutationCases()
        {
            yield return new object[] { Enumerable.Range(1, 3), 3, new int[][] { new int[] { 1, 2, 3 }, new int[] { 1, 3, 2 }, new int[] { 2, 1, 3 }, new int[] { 2, 3, 1 }, new int[] { 3, 1, 2 }, new int[] { 3, 2, 1 } } };
            yield return new object[] { Enumerable.Range(1, 3), 4, new int[][] { } };
        }

        [TestCaseSource(nameof(KCombinationsCases))]
        public void GeneratesKCombinations(IEnumerable<int> list, int length, IEnumerable<IEnumerable<int>> expectedResult)
        {
            // Arrange

            // Act
            var result = list.GetKCombinations(length);

            // Assert
            result.ShouldBe(expectedResult);
        }

        private static IEnumerable<object[]> KCombinationsCases()
        {
            yield return new object[] { Enumerable.Range(1, 4), 2, new int[][] { new int[] { 1, 2 }, new int[] { 1, 3 }, new int[] { 1, 4 }, new int[] { 2, 3 }, new int[] { 2, 4 }, new int[] { 3, 4 } } };
            yield return new object[] { Enumerable.Range(1, 3), 3, new int[][] { new int[] { 1, 2, 3 } } };
            yield return new object[] { Enumerable.Range(1, 3), 4, new int[][] { } };
        }
    }
}