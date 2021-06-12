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
            builder.Append(divider.Horizontal ? " H " : "H");
        }

        public void Visit(Square square)
        {
            var color = $"{Color.FromName("white").ToArgb():x6}";
            var value = square.SquareLeaf.IsLocked || !square.SquareLeaf.Value.Equals("0") ? square.SquareLeaf.Value : square.SquareLeaf.HelpValue;
            var content = value.Equals("0") || value.Equals("") ? "   " : $" {value} ";

            if (square.SquareLeaf.IsLocked && !square.SquareLeaf.IsSelected || !square.SquareLeaf.Value.Equals("0") && !square.SquareLeaf.IsSelected)
            {
                color = $"{Color.FromName("yellow").ToArgb():x6}";
            }
            else if (square.SquareLeaf.IsSelected)
            {
                color = $"{Color.FromName("cyan").ToArgb():x6}";
                content = value.Equals("0") || value.Equals("") ? " x " : $" {value} ";
            }

            color = square.SquareLeaf.Valid() || square.SquareLeaf.HelpValue.Equals("0") || square.SquareLeaf.IsLocked || square.SquareLeaf.IsSelected ? color : $"{Color.FromName("red").ToArgb():x6}";

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