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
        public virtual BaseSudoku CreateSudoku(string data)
        {
            return CreateSudokuOfSize(data);
        }

        private BaseSudoku CreateSudokuOfSize(string data)
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
            var maxWidth = boxWidth;
            var maxHeight = boxHeight;

            for (var i = 0; i < size; i++)
            {
                var minX = maxWidth - boxWidth;
                var minY = maxHeight - boxHeight;

                var leaves = new List<IComponent>();
                leaves.AddRange(GetBoxBoards(boards, new Position(minX, minY), new Position(maxWidth, maxHeight)));

                boxes.Add(new BoxComposite(leaves));

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

        private IEnumerable<SquareLeaf> GenerateBoards(string data, int boardSize)
        {
            var boards = new List<SquareLeaf>();
            var output = StringHelper.GetStringChunks(data, 1).ToList();
            var dataPointCount = data.Length;

            for (var y = 0; y < boardSize; ++y)
            {
                for (var x = 0; x < boardSize; ++x)
                {
                    var content = output[data.Length - dataPointCount--];
                    boards.Add(new SquareLeaf(content != "0", content, new Position(x, y)));
                }
            }

            return boards;
        }

        protected virtual IEnumerable<SquareLeaf> GetBoxBoards(IEnumerable<SquareLeaf> squares, Position minPosition, Position maxPosition)
        {
            return squares.Where(square => square.Position.X >= minPosition.X)
                .Where(square => square.Position.X < maxPosition.X)
                .Where(square => square.Position.Y >= minPosition.Y)
                .Where(square => square.Position.Y < maxPosition.Y)
                .ToList();
        }
    }
}