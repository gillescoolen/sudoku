namespace Sudoku.Domain.Models.Interfaces
{
    public interface IStrategy
    {
        public bool Solve(SudokuWrapper sudoku);
    }
}