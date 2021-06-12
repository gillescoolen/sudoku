using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models
{
    public class BoxComponent : IComponent
    {
        public List<IComponent> Children { get; }

        public BoxComponent(List<IComponent> children)
        {
            Children = children;
        }

        public bool Valid()
        {
            var children = Find(cell => !cell.IsComponent()).Cast<CellLeaf>();
            var cells = children as CellLeaf[] ?? children.ToArray();
            var doubles = cells.GroupBy(g => g.Value).Where(g => g.Count() > 1 && !g.Key.Equals("0"))
                .Select(g => g.Key);

            return doubles.Any();
        }

        public bool IsComponent()
        {
            return true;
        }

        public IEnumerable<IComponent> Find(Func<IComponent, bool> finder)
        {
            return GetChildren().Descendants(i => i.GetChildren()).Where(finder);
        }

        public IEnumerable<IComponent> GetChildren()
        {
            return Children;
        }
    }
}