using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Domain.Visitors
{
    public class HintVisitor : IVisitor
    {
        public Board Visit(SudokuWrapper puzzle)
        {
            var board = new Board();
            board.Add(new Divider(false));
            board.Add(new Divider(false));
            return board;
        }
    }
}