#nullable enable

using Sudoku.Domain.Models;
using Sudoku.Domain.Utilities;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.States
{
    public class HintState : State
    {
        public override void EnterValue(string value, CellLeaf cell)
        {
            cell.HelpValue = value;
        }

        public override Board? Construct()
        {
            return Context?.BaseSudoku()?.Accept(new NormalSudokuVisitor());
        }

        public override bool CheckEquality(CellLeaf leftCell, CellLeaf rightCell)
        {
            return HasCellValue(leftCell) && HasCellValue(rightCell) &&
                leftCell.HelpValue == rightCell.HelpValue ||
                !leftCell.Value.Equals("0") && leftCell.Value == rightCell.HelpValue ||
                !rightCell.Value.Equals("0") && rightCell.Value == leftCell.HelpValue;
        }

        public override bool HasCellValue(CellLeaf cell)
        {
            return !cell.HelpValue.Equals("0");
        }
    }
}