using Sudoku.Domain.Models;

namespace Sudoku.Domain.Factories
{
    public interface IAbstractSudokuFactory
    {
        public BaseSudoku CreateSudoku(string data);
    }
}