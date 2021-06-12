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

        public virtual void Select(Coordinate coordinate)
        {
            var orderedSquares = Context?.Sudoku()?.GetOrderedSquares();
            var currentLeaf = orderedSquares?.FirstOrDefault(squareLeaf => squareLeaf.IsSelected);
            if (currentLeaf == null || orderedSquares == null) return;

            var newCoordinate = new Coordinate(currentLeaf.Coordinate.X + coordinate.X, currentLeaf.Coordinate.Y + coordinate.Y);
            var newLeaf = orderedSquares.FirstOrDefault(squareLeaf =>
                squareLeaf.Coordinate.X == newCoordinate.X && squareLeaf.Coordinate.Y == newCoordinate.Y);

            if (newLeaf == null) return;

            currentLeaf.ToggleSelect();
            newLeaf.ToggleSelect();
        }
    }
}