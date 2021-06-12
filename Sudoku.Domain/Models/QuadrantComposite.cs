using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models
{
    public class BoxComposite : IComponent
    {
        public List<IComponent> Children { get; }

        public BoxComposite(List<IComponent> children)
        {
            Children = children;
        }

        public bool Valid(State state, bool setValid)
        {
            var children = Find(c => !c.IsComposite()).Cast<CellLeaf>();
            var cellLeaves = children as CellLeaf[] ?? children.ToArray();
            var doubles = cellLeaves.GroupBy(g => g.Value).Where(g => g.Count() > 1 && !g.Key.Equals("0"))
                .Select(g => g.Key);

            return doubles.Any();
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
            return Children;
        }
    }
}