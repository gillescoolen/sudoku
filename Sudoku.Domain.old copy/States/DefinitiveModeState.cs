#nullable enable

using Sudoku.Domain.Models;
using Sudoku.Domain.Utilities;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.States
{
    public class DefinitiveState : State
    {
        public override void EnterValue(string value, CellLeaf cell)
        {
            cell.Value = value;
        }

        public override Board? Construct()
        {
            return Context?.SudokuWrapper()?.Accept(new DefinitiveVisitor());
        }
    }
}