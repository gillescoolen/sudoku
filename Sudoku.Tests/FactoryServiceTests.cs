using System;
using System.Linq;
using NUnit.Framework;
using Sudoku.Domain.Models;
using Sudoku.Domain.Services;
using Sudoku.Domain.Utilities;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class FactoryServiceTests
    {
        private FactoryService factoryService;

        [OneTimeSetUp]
        public void Setup()
        {
            factoryService = new FactoryService();
        }

        [Test, TestCaseSource(typeof(CorrectBaseSudokuTypesTestData), nameof(CorrectBaseSudokuTypesTestData.TypesWithSudokuCount))]
        public void FactoryService_WithTypeAndData_ReturnsBaseSudokuWithCorrectAmountOfSudokus(string type, string data, int validSudokuCount)
        {
            var sudoku = factoryService.Create(type, data);

            Assert.IsNotNull(sudoku);
            Assert.AreEqual(validSudokuCount, sudoku.Sudokus.Count);
        }

        [Test, TestCaseSource(typeof(CorrectBaseSudokuTypesTestData), nameof(CorrectBaseSudokuTypesTestData.TypesWithLength))]
        public void FactoryService_WithTypeAndData_ReturnsBaseSudokuWithCorrectAmountOfSquares(string type, string data, int validSquareCount)
        {
            var sudoku = factoryService.Create(type, data);

            var squareCount = sudoku.Sudokus
                .SelectMany(quadrant => quadrant.Find(leaf => !leaf.IsComposite()))
                .Cast<SquareLeaf>()
                .Distinct(new SquareComparer())
                .ToList().Count;

            Assert.AreEqual(validSquareCount, squareCount);
        }

        [Test, TestCaseSource(typeof(CorrectBaseSudokuTypesTestData), nameof(CorrectBaseSudokuTypesTestData.TypesWithSolveStrategy))]
        public void FactoryService_WithTypeAndData_ReturnsBaseSudokuWithCorrectSolveStrategy(string type, string data, Type solveStrategy)
        {
            var sudoku = factoryService.Create(type, data);
            var actualStrategy = sudoku.GetSolverStrategy();

            Assert.AreEqual(solveStrategy, actualStrategy.GetType());
        }
    }
}