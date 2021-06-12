using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models.Parts
{
    public class Square : IPart
    {
        public SquareLeaf SquareLeaf { get; }

        public Square(SquareLeaf squareLeaf)
        {
            SquareLeaf = squareLeaf;
        }

        public void Accept(IDrawVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}