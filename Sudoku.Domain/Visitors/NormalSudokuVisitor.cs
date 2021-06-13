using System.Linq;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Visitors
{
    public class NormalSudokuVisitor : IVisitor
    {
        public Board Visit(BaseSudoku sudoku)
        {
            var builder = new BoardBuilder();
            var squares = sudoku.GetOrderedSquares();
            var quadrants = sudoku.Sudokus.SelectMany(c => c.Find(q => q.IsComposite())).ToList();
            var totalWidth = squares.Max(squareLeaf => squareLeaf.Coordinate.X) + 1;
            var firstQuadrant = sudoku.Sudokus.Find(c => c.IsComposite())!.GetChildren().Count();
            var nextHorizontal = firstQuadrant.FloorSqrt();
            var nextVertical = firstQuadrant.CeilingSqrt();

            for (var i = 0; i < squares.Count; ++i)
            {
                var leaf = squares[i];
                var nextLeaf = i + 1 > squares.Count - 1 ? null : squares[i + 1];
                var downLeaf = squares.FirstOrDefault(squareLeaf => squareLeaf.Coordinate.Y == leaf.Coordinate.Y + 1 && squareLeaf.Coordinate.X == leaf.Coordinate.X);
                var quadrant = quadrants.First(q => q.GetChildren().Contains(leaf));

                builder.BuildSquare(leaf);

                if (nextLeaf?.Coordinate.Y == leaf.Coordinate.Y && !quadrant.GetChildren().Contains(nextLeaf!))
                {
                    if (leaf.IsSpacingSquare() && nextLeaf.IsSpacingSquare())
                    {
                        builder.BuildSpacer(1);
                    }
                    else
                    {
                        builder.BuildDivider(false);
                    }
                }

                if (nextLeaf?.Coordinate.Y == leaf.Coordinate.Y) continue;

                builder.BuildRow();

                if ((leaf.Coordinate.Y + 1) % nextHorizontal != 0 || downLeaf == null) continue;

                var currentLeaves = squares.Where(squareLeaf => squareLeaf.Coordinate.Y == leaf.Coordinate.Y).ToList();
                var nextLeaves = squares.Where(squareLeaf => squareLeaf.Coordinate.Y == leaf.Coordinate.Y + 1).ToList();

                for (var wall = 0; wall < totalWidth; ++wall)
                {
                    if (!currentLeaves[wall].IsSpacingSquare() && !nextLeaves[wall].IsSpacingSquare())
                    {
                        builder.BuildDivider(true);
                    }
                    else
                    {
                        builder.BuildSpacer(3);
                    }

                    if ((wall + 1) % nextVertical == 0) builder.BuildSpacer(1);
                }

                builder.BuildRow();
            }

            return builder.GetResult();
        }
    }
}