using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Utilities
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Descendants<T>(this IEnumerable<T> source, 
            Func<T, IEnumerable<T>> descendBy)
        {
            foreach (var value in source)
            {
                yield return value;
 
                foreach (var child in descendBy(value).Descendants(descendBy))
                {
                    yield return child;
                }
            }
        }
        
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> collection, int batchSize)
        {
            var nextBatch = new List<T>(batchSize);
            
            foreach (var item in collection)
            {
                nextBatch.Add(item);
                if (nextBatch.Count != batchSize) continue;
                
                yield return nextBatch;
                nextBatch = new List<T>(batchSize);
            }

            if (nextBatch.Count > 0) {
                yield return nextBatch;
            }
        }
    }
}