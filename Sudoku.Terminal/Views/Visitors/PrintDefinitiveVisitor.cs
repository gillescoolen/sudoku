using System.Drawing;
using System.Text;
using Pastel;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Parts;

namespace Sudoku.Terminal.Views.Visitors
{
    public class PrintDefinitiveVisitor : IPrintVisitor
    {
        private StringBuilder stringBuilder;

        public PrintDefinitiveVisitor(StringBuilder stringBuilder)
        {
            this.stringBuilder = stringBuilder;
        }
        public void Visit(Divider divider)
        {
            stringBuilder.Append(divider.Horizontal ? " - " : "|");
        }

        public void Visit(Square square)
        {
            // var color = $"{Color.FromName("white").ToArgb():x6}";
            // var content = square.SquareLeaf.Value.Equals("0") || square.SquareLeaf.Value.Equals("") ? "   " : $" {square.SquareLeaf.Value} ";

            // if (square.SquareLeaf.IsLocked && !square.SquareLeaf.IsSelected)
            // {
            //     color = $"{Color.FromName("yellow").ToArgb():x6}";
            // }

            // if (square.SquareLeaf.IsSelected)
            // {
            //     color = $"{Color.FromName("cyan").ToArgb():x6}";
            //     content = square.SquareLeaf.Value.Equals("0") || square.SquareLeaf.Value.Equals("") ? " x " : $" {square.SquareLeaf.Value} ";
            // }

            // color = square.SquareLeaf.Valid() || square.SquareLeaf.IsLocked || square.SquareLeaf.IsSelected ? color : $"{Color.FromName("red").ToArgb():x6}";

            // stringBuilder.Append(content.Pastel(color));
            var font = "white";
            var text = square.SquareLeaf.Value.Equals("0") || square.SquareLeaf.Value.Equals("") ? "   " : $" {square.SquareLeaf.Value} ";

            if (square.SquareLeaf.IsLocked && !square.SquareLeaf.IsSelected)
            {
                font = "yellow";
            }
            else if (square.SquareLeaf.IsSelected)
            {
                font = "cyan";
                text = square.SquareLeaf.Value.Equals("0") || square.SquareLeaf.Value.Equals("") ? " x " : $" {square.SquareLeaf.Value} ";
            }

            font = square.SquareLeaf.Valid() || square.SquareLeaf.IsLocked || square.SquareLeaf.IsSelected ? font : "red";

            stringBuilder.Append(text.Pastel($"{Color.FromName(font).ToArgb():x6}"));
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