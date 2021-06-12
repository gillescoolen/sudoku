using System;
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
            var dividingNumber = data.Length.RoundSqrt();
            var boxes = GenerateBoxes(dividingNumber, data);
            var sudoku = new SudokuComposite(boxes);

            return new NormalSudoku(new List<IComponent> { sudoku });
        }

        private List<IComponent> GenerateBoxes(int size, string data)
        {
            var boards = GenerateBoards(data, size).ToList();

            var boxWidth = size.CalculateWidth();
            var boxHeight = size.CalculateHeight();

            var boxes = new List<IComponent>();

            var currentBox = 0;
            var maxX = boxWidth;
            var maxY = boxHeight;

            for (var i = 0; i < size; i++)
            {
                var minX = maxX - boxWidth;
                var minY = maxY - boxHeight;

                var leaves = new List<IComponent>();
                leaves.AddRange(GetBoxBoards(boards, new Position(minX, minY), new Position(maxX, maxY)));

                boxes.Add(new BoxComposite(leaves));

                currentBox++;

                if (currentBox == boxHeight)
                {
                    maxX = boxWidth;
                    maxY += boxHeight;
                    currentBox = 0;
                    continue;
                }

                maxX += boxWidth;
            }

            return boxes;
        }

        private IEnumerable<CellLeaf> GenerateBoards(string data, int boardSize)
        {
            var boards = new List<CellLeaf>();
            var contentList = StringHelper.GetStringChunks(data, 1).ToList();
            var dataPointCount = data.Length;

            for (var y = 0; y < boardSize; ++y)
            {
                for (var x = 0; x < boardSize; ++x)
                {
                    var content = contentList[data.Length - dataPointCount--];
                    boards.Add(new CellLeaf(content != "0", content, new Position(x, y)));
                }
            }

            return boards;
        }

        protected virtual IEnumerable<CellLeaf> GetBoxBoards(IEnumerable<CellLeaf> cells, Position minPosition, Position maxPosition)
        {
            return cells.Where(cell => cell.Position.X >= minPosition.X)
                .Where(cell => cell.Position.X < maxPosition.X)
                .Where(cell => cell.Position.Y >= minPosition.Y)
                .Where(cell => cell.Position.Y < maxPosition.Y).ToList();
        }
    }
}