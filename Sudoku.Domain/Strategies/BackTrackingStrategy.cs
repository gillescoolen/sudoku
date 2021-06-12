using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Strategies
{
    public class BackTrackingStrategy : IStrategy
    {
        public bool Solve(SudokuWrapper sudokuWrapper, State state)
        {
            var emptyCell = sudokuWrapper.GetOrderedCells().FirstOrDefault(c => c.Value.Equals("0") && !c.IsLocked);
            var maxValue = sudokuWrapper.MaxValue();

            if (emptyCell == null) return true;

            for (var i = 1; i <= maxValue; ++i)
            {
                emptyCell.Value = $"{i}";

                if (sudokuWrapper.ValidateSudoku(state) && Solve(sudokuWrapper, state))
                {
                    return true;
                }

                emptyCell.Value = "0";
            }

            return false;
        }
    }
}