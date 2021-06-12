using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models
{
    public abstract class SudokuWrapper
    {
        public List<IComponent> Sudokus { get; }

        public SudokuWrapper(List<IComponent> sudokus)
        {
            Sudokus = sudokus;
        }

        public virtual List<CellLeaf> GetOrderedCells()
        {
            return Sudokus.SelectMany(box => box.Find(leaf => !leaf.IsComponent()))
                .Cast<CellLeaf>()
                .OrderBy(c => c.Position.Y)
                .ThenBy(c => c.Position.X)
                .Distinct(new DistinctLeafComparer())
                .ToList();
        }

        public int MaxValue()
        {
            return Sudokus.First()
                .GetChildren()
                .Max(q => q.GetChildren().Count());
        }

        public Board Accept(IVisitor visitor)
        {
            return visitor.Visit(this);
        }

        public List<IComponent> GetFormats()
        {
            return Sudokus.Where(s => s.GetChildren().Count() > 2).ToList();
        }

        public abstract IStrategy GetSolveStratgy();

        public virtual bool ValidateSudoku()
        {
            foreach (var sudoku in GetFormats())
            {
                var cellLeaves = sudoku.GetChildren().SelectMany(
                    box => box.GetChildren()
                ).Cast<CellLeaf>().ToList();

                foreach (var cellLeaf in cellLeaves)
                {
                    if (cellLeaf.IsLocked || cellLeaf.Value.Equals("0")) continue;

                    var rowColumn = cellLeaves.Where(cell => (
                        cell.Position.Y == cellLeaf.Position.Y
                        || cell.Position.X == cellLeaf.Position.X)
                        && cell != cellLeaf
                    )
                    .FirstOrDefault(cell => cell.Value == cellLeaf.Value);

                    if (rowColumn != null) return false;

                    var box = sudoku.Find(c => c.GetChildren().Contains(cellLeaf)).First();

                    if (box.GetChildren().Cast<CellLeaf>().FirstOrDefault(c =>
                        c.Value == cellLeaf.Value
                        && c != cellLeaf) != null
                    )
                        return false;
                }
            }

            return true;
        }

        public virtual void ValidateBoard()
        {
            foreach (var sudoku in GetFormats())
            {
                var cells = sudoku.GetChildren().SelectMany(box => box.GetChildren()).Cast<CellLeaf>().ToList();

                foreach (var cell in cells)
                {
                    if (cell.IsLocked || cell.Value.Equals("0")) continue;

                    var rowColumn = cells.Where(c => (
                        c.Position.Y == cell.Position.Y
                        || c.Position.X == cell.Position.X)
                        && c != cell
                    )
                    .FirstOrDefault(c => c.Value == cell.Value);

                    if (rowColumn != null) cell.IsValid = false;

                    var box = sudoku.Find(c => c.GetChildren().Contains(cell)).First();

                    if (box.GetChildren().Cast<CellLeaf>().FirstOrDefault(c => c.Value == cell.Value && c != cell) != null) cell.IsValid = false;
                }
            }
        }
    }
}