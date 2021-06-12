using Sudoku.Domain.Models;

namespace Sudoku.Domain.Services
{
    public interface IFactoryService
    {
        public BaseSudoku Create(string type, string data);
    }
}