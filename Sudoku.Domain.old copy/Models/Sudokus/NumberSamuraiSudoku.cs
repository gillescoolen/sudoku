using System.Collections.Generic;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Strategies;

namespace Sudoku.Domain.Models.Sudokus
{
    public class NumberSamuraiSudokuComponent : SudokuWrapper
    {
        public NumberSamuraiSudokuComponent(List<IComponent> sudokus) : base(sudokus)
        {
        }

        public override IStrategy GetSolveStratgy()
        {
            return new BackTrackingStrategy();
        }
    }
}