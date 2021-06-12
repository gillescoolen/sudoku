using Sudoku.Domain.Models;

namespace Sudoku.Domain.Services
{
    public interface IFactoryService
    {
        public SudokuWrapper Create(string type, string data);
    }
}