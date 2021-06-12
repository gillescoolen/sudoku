#nullable enable

using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IContext
    {
        public State? GetState();
        public void SetSudoku(BaseSudoku? sudoku);
        public BaseSudoku? Sudoku();
        public void SwitchState(State newState);
        public void EnterValue(string value, SquareLeaf square);
        public Board CreateBoard();
        public void SetStrategy(IStrategy newStrategy);
        public IStrategy? GetStrategy();
    }
}