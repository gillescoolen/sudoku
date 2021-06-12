using System.Text;
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

        public void Accept(StringBuilder output, IDrawVisitor<StringBuilder> visitor)
        {
            visitor.Visit(output, this);
        }
    }
}