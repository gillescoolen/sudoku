using System.Collections.Generic;
using NUnit.Framework;
using Sudoku.Domain.Strategies;

namespace Sudoku.Tests.TestData
{
    public class CorrectBaseSudokuTypesTestData
    {
        public static readonly List<FormatData> Formats = new()
        {
            new FormatData("4x4", "0340400210030210"),
            new FormatData("6x6", "003010560320054203206450012045040100"),
            new FormatData("9x9", "700509001000000000150070063003904100000050000002106400390040076000000000600201004")
        };

        public static object[] TypesWithLength =
        {
            new object[] { Formats[0].Type, Formats[0].Data, 16 },
            new object[] { Formats[1].Type, Formats[1].Data, 36 },
            new object[] { Formats[2].Type, Formats[2].Data, 81 },
        };

        public static object[] TypesWithSudokuCount =
        {
            new object[] { Formats[0].Type, Formats[0].Data, 1 },
            new object[] { Formats[1].Type, Formats[1].Data, 1 },
            new object[] { Formats[2].Type, Formats[2].Data, 1 },
        };

        public static object[] TypesWithSolveStrategy =
        {
            new object[] { Formats[0].Type, Formats[0].Data, typeof(BackTrackStrategy) },
            new object[] { Formats[1].Type, Formats[1].Data, typeof(BackTrackStrategy) },
            new object[] { Formats[2].Type, Formats[2].Data, typeof(BackTrackStrategy) },
        };
    }

    public readonly struct FormatData
    {
        public FormatData(string type, string data)
        {
            Type = type;
            Data = data;
        }

        public string Type { get; }
        public string Data { get; }
    }
}