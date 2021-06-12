using System.Text;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IPart
    {
        public void Accept(StringBuilder output, IDrawVisitor<StringBuilder> visitor);
    }
}