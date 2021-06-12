using System.Drawing;
using System.Text;
using Pastel;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Terminal.Views.Visitors
{
    public class DrawDefinitiveVisitor : IDrawVisitor
    {
        private StringBuilder stringBuilder;

        public DrawDefinitiveVisitor(StringBuilder stringBuilder)
        {
            this.stringBuilder = stringBuilder;
        }
        public void Visit(Divider divider)
        {
            stringBuilder.Append(divider.Horizontal ? " - " : "|");
        }

        public void Visit(Cell cell)
        {
            var color = $"{Color.FromName("white").ToArgb():x6}";
            var content = cell.CellLeaf.Value.Equals("0") || cell.CellLeaf.Value.Equals("") ? "   " : $" {cell.CellLeaf.Value} ";

            if (cell.CellLeaf.IsLocked && !cell.CellLeaf.IsSelected)
            {
                color = $"{Color.FromName("yellow").ToArgb():x6}";
            }

            if (cell.CellLeaf.IsSelected)
            {
                color = $"{Color.FromName("cyan").ToArgb():x6}";
                content = cell.CellLeaf.Value.Equals("0") || cell.CellLeaf.Value.Equals("") ? " x " : $" {cell.CellLeaf.Value} ";
            }

            color = cell.CellLeaf.Valid() || cell.CellLeaf.IsLocked || cell.CellLeaf.IsSelected ? color : $"{Color.FromName("red").ToArgb():x6}";;

            stringBuilder.Append(content.Pastel(color));
        }

        public void Visit(Row row)
        {
            stringBuilder.AppendLine();
        }

        public void Visit(Spacer spacer)
        {
            stringBuilder.Append(new string(' ', spacer.Size));
        }
    }
}