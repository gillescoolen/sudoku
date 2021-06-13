using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Services
{
    public interface IFactoryService
    {
        public BaseSudoku Create(string format, string data);
    }
}