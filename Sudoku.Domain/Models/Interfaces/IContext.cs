#nullable enable

using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IContext
    {
        public State? GetState();
        public void SetSudokuWrapper(SudokuWrapper? sudoku);
        public SudokuWrapper? SudokuWrapper();
        public void TransitionTo(State newState);
        public void EnterValue(string value, CellLeaf cell);
        public Board Construct();
        public void SetStrategy(IStrategy newStrategy);
        public IStrategy? GetStrategy();
    }
}