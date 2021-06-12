using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models.Parts
{
    public class Cell : IPart
    {
        public CellLeaf CellLeaf { get; }

        public Cell(CellLeaf cellLeaf)
        {
            CellLeaf = cellLeaf;
        }

        public void Accept(IDrawVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}