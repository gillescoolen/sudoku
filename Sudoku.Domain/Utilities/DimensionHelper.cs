using System;

namespace Sudoku.Domain.Utilities
{
    public static class DimensionHelper
    {
        public static int CeilingSqrt(this int input) => BaseCalculation(Math.Ceiling, input);
        public static int FloorSqrt(this int input) => BaseCalculation(Math.Floor, input);
        public static int RoundSqrt(this int input) => BaseCalculation(Math.Round, input);

        private static int BaseCalculation(Func<double, double> mathFunc, int input) =>
            Convert.ToInt32(mathFunc(Math.Sqrt(input)));
    }
}