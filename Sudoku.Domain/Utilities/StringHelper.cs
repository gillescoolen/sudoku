using System.Collections.Generic;

namespace Sudoku.Domain.Utilities
{
    public static class StringHelper
    {
        public static string RemoveNewLineFromString(string input)
        {
            input = input.Replace("\r", "");
            return input.Replace("\n", "");
        }

        public static IEnumerable<string> GetStringChunks(string data, int chunkSize)
        {
            for (var i = 0; i < data.Length; i += chunkSize)
                yield return data.Substring(i, chunkSize);
        }
    }
}