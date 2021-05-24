using Sudoku.Domain
;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sudoku.Parser
{
    public class Parser
    {
        public Game Read(string filePath)
        {
            string gameString = System.IO.File.ReadAllText(filePath);

            Console.WriteLine(gameString);
            var game = new Game();


            return game;
        }
    }
}
