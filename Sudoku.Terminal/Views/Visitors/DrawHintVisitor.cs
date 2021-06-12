using System.Drawing;
using System.Text;
using Pastel;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Terminal.Views.Visitors
{
    public class DrawHintVisitor : IDrawVisitor
    {
        private StringBuilder builder;

        public DrawHintVisitor(StringBuilder builder)
        {
            this.builder = builder;
        }

        public void Visit(Divider divider)
        {
            builder.Append(divider.Horizontal ? " - " : "|");
        }

        public void Visit(Cell cell)
        {
            var color = $"{Color.FromName("white").ToArgb():x6}";
            var value = cell.CellLeaf.IsLocked || !cell.CellLeaf.Value.Equals("0") ? cell.CellLeaf.Value : cell.CellLeaf.HelpValue;
            var content = value.Equals("0") || value.Equals("") ? "   " : $" {value} ";

            if (cell.CellLeaf.IsLocked && !cell.CellLeaf.IsSelected || !cell.CellLeaf.Value.Equals("0") && !cell.CellLeaf.IsSelected)
            {
                color = $"{Color.FromName("yellow").ToArgb():x6}";
            }

            if (cell.CellLeaf.IsSelected)
            {
                color = $"{Color.FromName("cyan").ToArgb():x6}";
                content = value.Equals("0") || value.Equals("") ? " x " : $" {value} ";
            }

            color = cell.CellLeaf.Valid() || cell.CellLeaf.HelpValue.Equals("0") || cell.CellLeaf.IsLocked || cell.CellLeaf.IsSelected ? color : $"{Color.FromName("red").ToArgb():x6}";

            builder.Append(content.Pastel(color));
        }

        public void Visit(Row row)
        {
            builder.AppendLine();
        }

        public void Visit(Spacer spacer)
        {
            builder.Append(new string(' ', spacer.Size));
        }
    }
}