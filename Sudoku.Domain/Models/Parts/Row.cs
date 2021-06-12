using System.Text;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models.Parts
{
    public class Row : IPart
    {
        public void Accept(IDrawVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}