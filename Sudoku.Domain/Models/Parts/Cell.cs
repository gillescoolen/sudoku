using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models.Parts
{
    public class Square : IPart
    {
        public SquareLeaf SquareLeaf { get; }

        public Square(SquareLeaf square)
        {
            SquareLeaf = square;
        }

        public void Accept(IPrintVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}