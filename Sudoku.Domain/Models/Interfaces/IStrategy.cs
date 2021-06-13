using Sudoku.Domain.Utilities;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IStrategy
    {
        public bool Solve(BaseSudoku sudoku, State state);
    }
}