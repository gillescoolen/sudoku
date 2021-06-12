using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface ISudoku
    {
        public void InsertValueOnLocation(CellLeaf cellLeaf, string value);
        public List<IComponent> Boxes { get; }
        public IEnumerable<IComponent> Find(Func<IComponent, bool> finder);
        public IEnumerable<IComponent> GetChildren();
    }
}