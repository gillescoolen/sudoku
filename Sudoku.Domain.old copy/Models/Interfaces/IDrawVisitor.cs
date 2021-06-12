using Sudoku.Domain.Models.Parts;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IDrawVisitor
    {
        public void Visit(Divider Divider);
        public void Visit(Cell cell);
        public void Visit(Row row);
        public void Visit(Spacer spacer);
    }
}