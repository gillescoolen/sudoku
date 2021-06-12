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

        public abstract void EnterValue(string value, CellLeaf cell);
        public abstract Board? Construct();
        public abstract bool CheckEquality(CellLeaf leftCell, CellLeaf rightCell);
        public abstract bool HasCellValue(CellLeaf cell);

        public virtual void Select(Position position)
        {
            var orderedCells = Context?.BaseSudoku()?.GetOrderedCells();
            var currentLeaf = orderedCells?.FirstOrDefault(cellLeaf => cellLeaf.IsSelected);
            if (currentLeaf == null || orderedCells == null) return;

            var newPosition = new Position(currentLeaf.Position.X + position.X, currentLeaf.Position.Y + position.Y);
            var newLeaf = orderedCells.FirstOrDefault(cellLeaf =>
                cellLeaf.Position.X == newPosition.X && cellLeaf.Position.Y == newPosition.Y);

            if (newLeaf == null) return;

            currentLeaf.ToggleSelect();
            newLeaf.ToggleSelect();
        }
    }
}