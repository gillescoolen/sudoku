#nullable enable

using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Utilities
{
    public class Context : IContext
    {
        private State? state;
        private IStrategy? strategy;
        private BaseSudoku? sudoku;

        public State? GetState()
        {
            return state;
        }

        public void SetSudoku(BaseSudoku? sudoku)
        {
            this.sudoku = sudoku;
            if (this.sudoku == null) return;
            SetStrategy(sudoku!.GetSolverStrategy());
        }

        public BaseSudoku? Sudoku()
        {
            return sudoku;
        }

        public IStrategy? GetStrategy()
        {
            return strategy;
        }

        public void SwitchState(State newState)
        {
            state = newState;
            state.SetContext(this);
        }

        public void EnterValue(string value, SquareLeaf square)
        {
            state?.EnterValue(value, square);
        }

        public Board CreateBoard()
        {
            return state?.Construct() ?? new Board();
        }

        public void SetStrategy(IStrategy newStrategy)
        {
            strategy = newStrategy;
        }
    }
}