using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models.Parts
{
    public class Spacer : IPart
    {
        public int Size { get; }

        public Spacer(int size)
        {
            Size = size;
        }

        public void Accept(IPrintVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}