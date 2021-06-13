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
            var currentSquare = orderedSquares?.FirstOrDefault(square => square.IsSelected);

            if (currentSquare == null || orderedSquares == null) return;

            var nextCoordinate = new Coordinate(currentSquare.Coordinate.X + coordinate.X, currentSquare.Coordinate.Y + coordinate.Y);

            var nextSquare = orderedSquares.FirstOrDefault(square => square.Coordinate.X == nextCoordinate.X && square.Coordinate.Y == nextCoordinate.Y);

            if (nextSquare == null) return;

            currentSquare.ToggleSelect();
            nextSquare.ToggleSelect();
        }
    }
}