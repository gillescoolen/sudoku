using System.Collections.Generic;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Strategies;

namespace Sudoku.Domain.Models.Sudokus
{
    public class NumberSudoku : SudokuWrapper
    {
        public NumberSudoku(List<IComponent> sudokus) : base(sudokus)
        {
        }

        public override IStrategy GetSolveStratgy()
        {
            return new BackTrackingStrategy();
        }
    }
}