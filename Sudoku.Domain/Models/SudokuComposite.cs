using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models
{
    public class SudokuComposite : IComponent
    {
        public List<IComponent> Boxes { get; }

        public SudokuComposite(List<IComponent> boxes)
        {
            Boxes = boxes;
        }

        public bool Valid(State state, bool setValid)
        {
            var squares = GetChildren().SelectMany(q => q.GetChildren()).Cast<SquareLeaf>().ToList();

            foreach (var square in squares.Where(square => !square.IsLocked && state.HasSquareValue(square)))
            {
                var rowColumn = squares.Where(c => (c.Position.Y == square.Position.Y || c.Position.X == square.Position.X) && c != square)
                    .FirstOrDefault(c => state.CheckEquality(c, square));

                if (rowColumn != null)
                {
                    if (setValid) square.IsValid = false;
                    else
                    {
                        return false;
                    }
                }

                var box = Find(c => c.GetChildren().Contains(square)).First();

                if (box.GetChildren().Cast<SquareLeaf>()
                    .FirstOrDefault(c => state.CheckEquality(c, square) && c != square) == null) continue;

                if (setValid) square.IsValid = false;
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
            return Boxes;
        }
    }
}