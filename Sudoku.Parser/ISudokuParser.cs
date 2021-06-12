using Sudoku.Domain.Models;

namespace Sudoku.Parser
{
    public interface ISudokuParser
    {
        public BaseSudoku Parse(string format);
    }
}