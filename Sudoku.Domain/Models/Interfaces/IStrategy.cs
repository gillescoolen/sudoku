using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IStrategy
    {
        public bool Solve(BaseSudoku baseSudoku, State state);
    }
}