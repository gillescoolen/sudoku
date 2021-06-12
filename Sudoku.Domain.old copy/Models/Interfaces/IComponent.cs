using System;
using System.Collections.Generic;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IComponent
    {
        public bool Valid();
        public bool IsComponent();
        public IEnumerable<IComponent> Find(Func<IComponent, bool> finder);
        public IEnumerable<IComponent> GetChildren();
    }
}