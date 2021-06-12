using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Strategies
{
    public class BackTrackingStrategy : IStrategy
    {
        public bool Solve(BaseSudoku baseSudoku, State state)
        {
            var emptyCell = baseSudoku.GetOrderedCells().FirstOrDefault(c => c.Value.Equals("0") && !c.IsLocked);
            var maxValue = baseSudoku.MaxValue();

            if (emptyCell == null) return true;

            for (var i = 1; i <= maxValue; ++i)
            {
                emptyCell.Value = $"{i}";
                if (baseSudoku.ValidateSudoku(state) && Solve(baseSudoku, state)) return true;
                emptyCell.Value = "0";
            }
            return false;
        }
    }
}