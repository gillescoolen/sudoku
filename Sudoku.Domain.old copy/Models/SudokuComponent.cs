using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models
{
    public class SudokuComponent : ISudoku, IComponent
    {
        public List<IComponent> Boxes { get; }

        public SudokuComponent(List<IComponent> boxes)
        {
            Boxes = boxes;
        }

        public void InsertValueOnLocation(CellLeaf cellLeaf, string value)
        {
            throw new System.NotImplementedException();
        }

        public bool Valid()
        {
            throw new System.NotImplementedException();
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
            return Boxes;
        }
    }
}