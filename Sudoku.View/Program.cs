using System;
using Sudoku.Parser;
using Sudoku.Domain;
namespace Sudoku.View
{
    class Program
    {
        public Game game;

        public static void Main(string[] args)
        {
            new Program();
        }
        private Program() {
            Console.Title = "Sudoku";

            var gameParser = new Parser.Parser();
            game = gameParser.Read("./levels/puzzle.4x4");
        }
      
    }
}
