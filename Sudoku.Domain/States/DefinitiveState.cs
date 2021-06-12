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
            return Context?.BaseSudoku()?.Accept(new NormalSudokuVisitor());
        }

        public override bool CheckEquality(CellLeaf leftCell, CellLeaf rightCell)
        {
            return leftCell.Value == rightCell.Value;
        }

        public override bool HasCellValue(CellLeaf cell)
        {
            return !cell.Value.Equals("0");
        }
    }
}