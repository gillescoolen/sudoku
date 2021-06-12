using System;
using System.Collections.Generic;
using System.Linq;
using Sudoku.Domain.Models;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Models.Sudokus;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Factories
{
    public class SudokuSamuraiFactory : SudokuNormalFactory
    {
        private static readonly List<List<int>> boxesPerSudoku = new()
        {
            new List<int> { 0, 1, 2, 7, 8, 9, 14, 15, 16 },
            new List<int> { 3, 10 },
            new List<int> { 4, 5, 6, 11, 12, 13, 18, 19, 20 },
            new List<int> { 21, 22 },
            new List<int> { 16, 17, 18, 23, 24, 25, 30, 31, 32 },
            new List<int> { 26, 27 },
            new List<int> { 28, 29, 30, 35, 36, 37, 42, 43, 44 },
            new List<int> { 38, 45 },
            new List<int> { 32, 33, 34, 39, 40, 41, 46, 47, 48 }
        };

        public override BaseSudoku CreateSudoku(string data)
        {
            var subSudokusString = StringHelper.RemoveNewLineFromString(data.Split(Environment.NewLine).First());

            var amountOfSquaresPerSudoku = subSudokusString.Length;
            var sizeOfSubSudokus = amountOfSquaresPerSudoku.RoundSqrt();

            var reOrderedData = ReOrderString(data, sizeOfSubSudokus);

            var spacer = sizeOfSubSudokus.RoundSqrt();
            var sizeOfTotal = sizeOfSubSudokus * 2 + spacer;
            var amountOfBoxesPerRow = sizeOfTotal / spacer;
            var amountOfBoxes = amountOfBoxesPerRow * amountOfBoxesPerRow;

            var boards = GenerateBoards(sizeOfTotal, spacer, sizeOfSubSudokus, reOrderedData).ToList();
            var boxes = GenerateBoxes(amountOfBoxes, sizeOfSubSudokus, boards);
            var sudokus = GenerateSudokus(boxes);

            return new SamuraiSudoku(sudokus);
        }

        private string ReOrderString(string input, int subSudokuSize)
        {
            var sudokuStrings = input
                .Split(Environment.NewLine)
                .Select(StringHelper.RemoveNewLineFromString)
                .Select(s => StringHelper.GetStringChunks(s, subSudokuSize).ToList()).ToList();

            var sudoku1 = sudokuStrings[0];
            var sudoku2 = sudokuStrings[1];
            var sudoku3 = sudokuStrings[2];
            var sudoku4 = sudokuStrings[3];
            var sudoku5 = sudokuStrings[4];

            var actualString = "";

            for (var i = 0; i <= 5; i++)
            {
                actualString += sudoku1[i] + sudoku2[i];
            }

            for (var i = 6; i <= 8; i++)
            {
                var subStringList = StringHelper.GetStringChunks(sudoku3[i - 6], 3).ToList();

                actualString += sudoku1[i] + subStringList[1] + sudoku2[i];
            }

            for (var i = 3; i <= 5; i++)
            {
                actualString += sudoku3[i];
            }

            for (var i = 0; i <= 2; i++)
            {
                var subStringList = StringHelper.GetStringChunks(sudoku3[i + 6], 3).ToList();

                actualString += sudoku4[i] + subStringList[1] + sudoku5[i];
            }

            for (var i = 3; i <= 8; i++)
            {
                actualString += sudoku4[i] + sudoku5[i];
            }

            return actualString;
        }

        private IEnumerable<SquareLeaf> GenerateBoards(int size, int spacerSize, int sizeOfSubs, string data)
        {
            var boards = new List<SquareLeaf>();

            var contentList = StringHelper.GetStringChunks(data, 1).ToList();
            var dataPointCount = data.Length;

            var startSpacerXy = sizeOfSubs;
            var endSpacerXy = sizeOfSubs + spacerSize;
            var spacerLength = sizeOfSubs - spacerSize;

            for (var y = 0; y < size; y++)
            {
                for (var x = 0; x < size; x++)
                {
                    var position = new Position(x, y);

                    if (x >= startSpacerXy && x < endSpacerXy && (y >= 0 && y < spacerLength || y >= size - spacerLength && y < size))
                    {
                        boards.Add(new SquareLeaf(true, position));
                    }
                    else if (y >= startSpacerXy && y < endSpacerXy && (x >= 0 && x < spacerLength || x >= size - spacerLength && x < size))
                    {
                        boards.Add(new SquareLeaf(true, position));
                    }
                    else
                    {
                        var content = contentList[data.Length - dataPointCount--];
                        boards.Add(new SquareLeaf(content != "0", content, position));
                    }
                }
            }

            return boards;
        }

        private List<BoxComposite> GenerateBoxes(int amountOfBoxes, int sizeOfSubs, List<SquareLeaf> boards)
        {
            var boxWidth = sizeOfSubs.CalculateWidth();
            var boxHeight = sizeOfSubs.CalculateHeight();

            var boxesPerRow = amountOfBoxes.RoundSqrt();

            var boxes = new List<BoxComposite>();

            var currentBox = 0;
            var maxX = boxWidth;
            var maxY = boxHeight;

            for (var i = 0; i < amountOfBoxes; i++)
            {
                var minX = maxX - boxWidth;
                var minY = maxY - boxHeight;

                var leaves = new List<IComponent>();
                leaves.AddRange(GetBoxBoards(boards, new Position(minX, minY), new Position(maxX, maxY)));

                boxes.Add(new BoxComposite(leaves));

                currentBox++;

                if (currentBox == boxesPerRow)
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

        private List<IComponent> GenerateSudokus(IReadOnlyList<IComponent> boxes)
        {
            return boxesPerSudoku
                .Select(ints => ints.Select(i => boxes[i]).ToList())
                .Select(sudokuBoxes => new SudokuComposite(sudokuBoxes))
                .Cast<IComponent>()
                .ToList();
        }
    }
}