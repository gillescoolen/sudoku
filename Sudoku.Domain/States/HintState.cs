#nullable enable

using Sudoku.Domain.Models;
using Sudoku.Domain.Utilities;
using Sudoku.Domain.Visitors;

namespace Sudoku.Domain.States
{
    public class HintState : State
    {
        public override void EnterValue(string value, SquareLeaf square)
        {
            square.HelpValue = value;
        }

        public override Board? Construct()
        {
            return Context?.Sudoku()?.Accept(new NormalSudokuVisitor());
        }

        public override bool CheckEquality(SquareLeaf leftSquare, SquareLeaf rightSquare)
        {
            return HasSquareValue(leftSquare) && HasSquareValue(rightSquare) &&
                leftSquare.HelpValue == rightSquare.HelpValue ||
                !leftSquare.Value.Equals("0") && leftSquare.Value == rightSquare.HelpValue ||
                !rightSquare.Value.Equals("0") && rightSquare.Value == leftSquare.HelpValue;
        }

        public override bool HasSquareValue(SquareLeaf square)
        {
            return !square.HelpValue.Equals("0");
        }
    }
}