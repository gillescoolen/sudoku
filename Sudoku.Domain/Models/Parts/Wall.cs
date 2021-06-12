using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models.Parts
{
    public class Divider : IPart
    {
        public bool Horizontal { get; }

        public Divider(bool horizontal)
        {
            Horizontal = horizontal;
        }

        public void Accept(IDrawVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}