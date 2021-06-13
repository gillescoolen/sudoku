using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Sudoku.Parser;
using Sudoku.Tests.TestData;

namespace Sudoku.Tests
{
    [TestFixture]
    public class FileReaderTests
    {
        private SudokuParser sudokuParser;
        private static string path = "../../../Formats/";
        private static List<string> types = CorrectBaseSudokuTypesTestData.Formats.Select(td => td.Type).ToList();

        [OneTimeSetUp]
        public void Setup()
        {
            sudokuParser = new SudokuParser();
        }

        [Test, TestCaseSource(nameof(types))]
        public void FileReader_WithType_ReturnsFileContent(string type)
        {
            var input = sudokuParser.Parse(type, path);
            Assert.NotNull(input);
        }
    }
}