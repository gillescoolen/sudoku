using System.Linq;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Strategies
{
    public class BackTrackStrategy : IStrategy
    {
        public bool Solve(BaseSudoku sudoku, State state)
        {
            var emptySquare = sudoku.GetOrderedSquares().FirstOrDefault(c => c.Value.Equals("0") && !c.IsLocked);
            var maxValue = sudoku.MaxValue();

            if (emptySquare == null) return true;

            for (var i = 1; i <= maxValue; ++i)
            {
                emptySquare.Value = $"{i}";

                if (sudoku.ValidateSudoku(state) && Solve(sudoku, state))
                {
                    return true;
                }

                emptySquare.Value = "0";
            }
            return false;
        }
    }
}