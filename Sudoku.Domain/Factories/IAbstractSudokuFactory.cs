using Sudoku.Domain.Models;

namespace Sudoku.Domain.Factories
{
    public interface IAbstractSudokuFactory
    {
        public SudokuWrapper CreateSudoku(string data);
    }
}