using System.Linq;
using Sudoku.Domain.Builders;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Visitors
{
    public class NormalSudokuVisitor : IVisitor
    {
        public Board Visit(BaseSudoku baseSudoku)
        {
            var builder = new BoardBuilder();
            var cells = baseSudoku.GetOrderedCells();
            var boxes = baseSudoku.Sudokus.SelectMany(c => c.Find(q => q.IsComposite())).ToList();
            var totalWidth = cells.Max(cellLeaf => cellLeaf.Position.X) + 1;
            var firstBox = baseSudoku.Sudokus.Find(c => c.IsComposite())!.GetChildren().Count();
            var nextHorizontal = firstBox.FloorSqrt();
            var nextVertical = firstBox.CeilingSqrt();

            for (var i = 0; i < cells.Count; ++i)
            {
                var leaf = cells[i];
                var nextLeaf = i + 1 > cells.Count - 1 ? null : cells[i + 1];
                var downLeaf = cells.FirstOrDefault(cellLeaf => cellLeaf.Position.Y == leaf.Position.Y + 1 && cellLeaf.Position.X == leaf.Position.X);
                var box = boxes.First(q => q.GetChildren().Contains(leaf));

                builder.BuildCell(leaf);

                if (nextLeaf?.Position.Y == leaf.Position.Y && !box.GetChildren().Contains(nextLeaf!))
                {
                    if (leaf.IsSpacingCell() && nextLeaf.IsSpacingCell())
                        builder.BuildSpacer(1);
                    else
                        builder.BuildDivider(false);
                }

                if (nextLeaf?.Position.Y == leaf.Position.Y) continue;

                builder.BuildRow();

                if ((leaf.Position.Y + 1) % nextHorizontal != 0 || downLeaf == null) continue;

                var currentLeaves = cells.Where(cellLeaf => cellLeaf.Position.Y == leaf.Position.Y).ToList();
                var nextLeaves = cells.Where(cellLeaf => cellLeaf.Position.Y == leaf.Position.Y + 1).ToList();

                for (var Divider = 0; Divider < totalWidth; ++Divider)
                {
                    if (!currentLeaves[Divider].IsSpacingCell() && !nextLeaves[Divider].IsSpacingCell())
                        builder.BuildDivider(true);
                    else
                        builder.BuildSpacer(3);

                    if ((Divider + 1) % nextVertical == 0) builder.BuildSpacer(1);
                }

                builder.BuildRow();
            }

            return builder.GetProduct();
        }
    }
}