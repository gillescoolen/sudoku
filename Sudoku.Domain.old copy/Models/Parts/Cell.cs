using System.Text;
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

        public void Accept(StringBuilder output, IDrawVisitor<StringBuilder> visitor)
        {
            visitor.Visit(output, this);
        }
    }
}