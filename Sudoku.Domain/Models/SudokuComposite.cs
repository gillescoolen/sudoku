using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models
{
    public class SudokuComposite : IComponent
    {
        public List<IComponent> Boxs { get; }

        public SudokuComposite(List<IComponent> boxs)
        {
            Boxs = boxs;
        }

        public bool Valid(State state, bool setValid)
        {
            var cells = GetChildren().SelectMany(q => q.GetChildren()).Cast<CellLeaf>().ToList();

            foreach (var cell in cells.Where(cell => !cell.IsLocked && state.HasCellValue(cell)))
            {
                var rowColumn = cells.Where(c => (c.Position.Y == cell.Position.Y || c.Position.X == cell.Position.X) && c != cell)
                    .FirstOrDefault(c => state.CheckEquality(c, cell));

                if (rowColumn != null)
                {
                    if (setValid) cell.IsValid = false;
                    else
                    {
                        return false;
                    }
                }

                var box = Find(c => c.GetChildren().Contains(cell)).First();

                if (box.GetChildren().Cast<CellLeaf>()
                    .FirstOrDefault(c => state.CheckEquality(c, cell) && c != cell) == null) continue;

                if (setValid) cell.IsValid = false;
                else
                {
                    return false;
                }
            }

            return true;
        }

        public bool IsComposite()
        {
            return true;
        }

        public IEnumerable<IComponent> Find(Func<IComponent, bool> finder)
        {
            return GetChildren().Descendants(i => i.GetChildren()).Where(finder);
        }

        public IEnumerable<IComponent> GetChildren()
        {
            return Boxs;
        }
    }
}