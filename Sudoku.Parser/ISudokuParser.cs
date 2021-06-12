using Sudoku.Domain.Models;

namespace Sudoku.Parser
{
    public interface ISudokuParser
    {
        public SudokuWrapper Parse(string format);
    }
}