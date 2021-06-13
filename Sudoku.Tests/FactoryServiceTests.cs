using System;
using System.Linq;
using NUnit.Framework;
using Sudoku.Domain.Models;
using Sudoku.Domain.Services;
using Sudoku.Domain.Utils;
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

        [Test, TestCaseSource(typeof(SudokuFormatData), nameof(SudokuFormatData.FormatsWithSolver))]
        public void Factory_Has_Solver(string type, string data, Type strategyType)
        {
            var sudoku = factoryService.Create(type, data);
            var strategy = sudoku.GetSolverStrategy();

            Assert.AreEqual(strategyType, strategy.GetType());
        }

        [Test, TestCaseSource(typeof(SudokuFormatData), nameof(SudokuFormatData.FormatsWithLengths))]
        public void Factory_Has_Squares(string type, string data, int count)
        {
            var sudoku = factoryService.Create(type, data);

            var squareCount = sudoku.Components
                .SelectMany(box => box.Find(square => !square.Composite()))
                .Cast<SquareLeaf>()
                .Distinct(new SquareComparer())
                .ToList().Count;

            Assert.AreEqual(count, squareCount);
        }
    }
}