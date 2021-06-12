using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Strategies
{
    public class BackTrackingStrategy : IStrategy
    {
        public bool Solve(SudokuWrapper sudoku)
        {
            var cell = sudoku.GetOrderedCells().FirstOrDefault(cell => cell.Value.Equals("0") && !cell.IsLocked);
            var maxValue = sudoku.MaxValue();

            if (cell == null) return true;

            for (var potentialValue = 1; potentialValue <= maxValue; ++potentialValue)
            {
                cell.Value = $"{potentialValue}";

                if (sudoku.ValidateSudoku() && Solve(sudoku)) return true;

                cell.Value = "0";
            }

            return false;
        }
    }
}