using System;
using System.Collections.Generic;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IComponent
    {
        public bool Valid(State state, bool setValid);
        public bool IsComposite();
        public IEnumerable<IComponent> Find(Func<IComponent, bool> search);
        public IEnumerable<IComponent> GetChildren();
    }
}