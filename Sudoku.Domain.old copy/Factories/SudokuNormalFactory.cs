using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Factories
{
    public class SudokuNormalFactory : IAbstractSudokuFactory
    {
        public virtual SudokuWrapper CreateSudoku(string data)
        {
            return CreateSudokuOfSize(data);
        }

        private SudokuWrapper CreateSudokuOfSize(string data)
        {
            var divider = data.Length.RoundSqrt();
            var boxes = GenerateBoxes(divider, data);
            var sudoku = new SudokuComponent(boxes);

            return new NumberSudokuComponent(new List<IComponent> { sudoku });
        }

        private List<IComponent> GenerateBoxes(int size, string data)
        {
            var boards = GenerateBoards(data, size).ToList();

            var boxWidth = size.CalculateWidth();
            var boxHeight = size.CalculateHeight();

            var boxes = new List<IComponent>();

            var currentBox = 0;
            var maxWidth = boxWidth;
            var maxHeight = boxHeight;

            for (var i = 0; i < size; i++)
            {
                var minWidth = maxWidth - boxWidth;
                var minHeight = maxHeight - boxHeight;

                var leaves = new List<IComponent>();
                leaves.AddRange(GetBoards(boards, new Position(minWidth, minHeight), new Position(maxWidth, maxHeight)));

                boxes.Add(new BoxComponent(leaves));

                currentBox++;

                if (currentBox == boxHeight)
                {
                    maxWidth = boxWidth;
                    maxHeight += boxHeight;
                    currentBox = 0;
                    continue;
                }

                maxWidth += boxWidth;
            }

            return boxes;
        }

        private IEnumerable<CellLeaf> GenerateBoards(string data, int boardSize)
        {
            var boards = new List<CellLeaf>();
            var contentList = StringHelper.GetStringChunks(data, 1).ToList();
            var size = data.Length;

            for (var y = 0; y < boardSize; ++y)
            {
                for (var x = 0; x < boardSize; ++x)
                {
                    var content = contentList[data.Length - size--];
                    boards.Add(new CellLeaf(content != "0", content, new Position(x, y)));
                }
            }

            return boards;
        }

        protected virtual IEnumerable<CellLeaf> GetBoards(IEnumerable<CellLeaf> cells, Position minPosition, Position maxPosition)
        {
            return cells.Where(cell => cell.Position.X >= minPosition.X)
                .Where(cell => cell.Position.X < maxPosition.X)
                .Where(cell => cell.Position.Y >= minPosition.Y)
                .Where(cell => cell.Position.Y < maxPosition.Y).ToList();
        }
    }
}