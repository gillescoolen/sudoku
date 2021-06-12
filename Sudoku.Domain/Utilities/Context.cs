#nullable enable

using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Utilities
{
    public class Context : IContext
    {
        private State? state;
        private IStrategy? strategy;
        private SudokuWrapper? sudokuWrapper;

        public State? GetState()
        {
            return state;
        }

        public void SetSudokuWrapper(SudokuWrapper? sudoku)
        {
            sudokuWrapper = sudoku;
            if (sudokuWrapper == null) return;
            SetStrategy(sudoku!.GetSolverStrategy());
        }

        public SudokuWrapper? SudokuWrapper()
        {
            return sudokuWrapper;
        }

        public IStrategy? GetStrategy()
        {
            return strategy;
        }

        public void TransitionTo(State newState)
        {
            state = newState;
            state.SetContext(this);
        }

        public void EnterValue(string value, CellLeaf cell)
        {
            state?.EnterValue(value, cell);
        }

        public Board Construct()
        {
            return state?.Construct() ?? new Board();
        }

        public void SetStrategy(IStrategy newStrategy)
        {
            strategy = newStrategy;
        }
    }
}