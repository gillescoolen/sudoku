using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Services
{
    public interface IFactoryService
    {
        public BaseSudoku Create(string type, string data);
    }
}