#nullable enable

using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Utilities
{
    public abstract class State
    {
        protected IContext? Context;

        public void SetContext(IContext context)
        {
            Context = context;
        }

        public abstract void EnterValue(string value, SquareLeaf square);
        public abstract Board? Construct();
        public abstract bool CheckEquality(SquareLeaf leftSquare, SquareLeaf rightSquare);
        public abstract bool HasSquareValue(SquareLeaf square);

        public virtual void Select(Position position)
        {
            var orderedSquares = Context?.BaseSudoku()?.GetOrderedSquares();
            var currentLeaf = orderedSquares?.FirstOrDefault(squareLeaf => squareLeaf.IsSelected);
            if (currentLeaf == null || orderedSquares == null) return;

            var newPosition = new Position(currentLeaf.Position.X + position.X, currentLeaf.Position.Y + position.Y);
            var newLeaf = orderedSquares.FirstOrDefault(squareLeaf =>
                squareLeaf.Position.X == newPosition.X && squareLeaf.Position.Y == newPosition.Y);

            if (newLeaf == null) return;

            currentLeaf.ToggleSelect();
            newLeaf.ToggleSelect();
        }
    }
}