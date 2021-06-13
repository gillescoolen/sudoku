#nullable enable

using System;
using System.Collections.Generic;
using Sudoku.Domain.Utilities;
using Sudoku.Domain.Models.Sudokus;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IGame : IObservable<Game>
    {
        public IContext GetContext();
        public void SwitchState(State state);
        public BaseSudoku? sudoku { get; set; }
        public Board Board { get; }
        public void SelectSquare(Coordinate coordinate);
        public void EnterValue(string value);
        public void Solve();
        public void ValidateSudoku(bool update = true);
        public List<(string type, bool selected)> GetFormats();
        public void SelectFormat(List<(string type, bool selected)> selection);
    }
}