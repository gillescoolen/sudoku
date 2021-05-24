using Sudoku.Domain;
using Sudoku.Domain.Models;
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

            Cell[] array = new Cell[] { new Cell(1, 1, new Position(1, 1, 1)), new Cell(1, 1, new Position(1, 1, 1)) };
            var game = new Game(array);


            return game;
        }
    }
}
