using System;
using System.Collections.Generic;

namespace Day13
{
    public static class IListExtensions
    {
        public static IEnumerable<List<T>> SplitList<T>(this List<T> collection, int nSize)
        {
            for (int i = 0; i < collection.Count; i += nSize)
            {
                yield return collection.GetRange(i, Math.Min(nSize, collection.Count - i));
            }
        }
    }
}