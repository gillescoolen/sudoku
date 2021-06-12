#nullable enable

using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IContext
    {
        public State? GetState();
        public void SetBaseSudoku(BaseSudoku? sudoku);
        public BaseSudoku? BaseSudoku();
        public void TransitionTo(State newState);
        public void EnterValue(string value, SquareLeaf square);
        public Board Construct();
        public void SetStrategy(IStrategy newStrategy);
        public IStrategy? GetStrategy();
    }
}