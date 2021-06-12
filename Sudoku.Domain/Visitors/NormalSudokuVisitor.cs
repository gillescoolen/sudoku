using System.Linq;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Visitors
{
    public class NormalSudokuVisitor : IVisitor
    {
        public Board Visit(BaseSudoku sudoku)
        {
            var builder = new BoardBuilder();

            var boxes = sudoku.Sudokus.SelectMany(c => c.Find(q => q.IsComposite())).ToList();

            var squares = sudoku.GetOrderedSquares();
            var totalWidth = squares.Max(square => square.Coordinate.X) + 1;

            var firstBox = sudoku.Sudokus.Find(c => c.IsComposite())!.GetChildren().Count();
            var nextHorizontal = firstBox.FloorSqrt();
            var nextVertical = firstBox.CeilingSqrt();

            for (var i = 0; i < squares.Count; ++i)
            {
                var square = squares[i];
                var nextSquare = i + 1 > squares.Count - 1 ? null : squares[i + 1];
                var bottomSquare = squares.FirstOrDefault(square => square.Coordinate.Y == square.Coordinate.Y + 1 && square.Coordinate.X == square.Coordinate.X);
                var box = boxes.First(q => q.GetChildren().Contains(square));

                builder.BuildSquare(square);

                if (nextSquare?.Coordinate.Y == square.Coordinate.Y && !box.GetChildren().Contains(nextSquare!))
                {
                    if (square.IsSpacingSquare() && nextSquare.IsSpacingSquare()) builder.BuildSpacer(1);
                    else builder.BuildDivider(false);
                }

                if (nextSquare?.Coordinate.Y == square.Coordinate.Y) continue;

                builder.BuildRow();

                if ((square.Coordinate.Y + 1) % nextHorizontal != 0 || bottomSquare == null) continue;

                var currentLeaves = squares
                    .Where(square => square.Coordinate.Y == square.Coordinate.Y)
                    .ToList();
                var nextLeaves = squares
                    .Where(square => square.Coordinate.Y == square.Coordinate.Y + 1)
                    .ToList();

                for (var Divider = 0; Divider < totalWidth; ++Divider)
                {
                    if (!currentLeaves[Divider].IsSpacingSquare() && !nextLeaves[Divider].IsSpacingSquare()) builder.BuildDivider(true);
                    else builder.BuildSpacer(3);

                    if ((Divider + 1) % nextVertical == 0) builder.BuildSpacer(1);
                }

                builder.BuildRow();
            }

            return builder.GetProduct();
        }
    }
}