#nullable enable

using System;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models.Interfaces
{
    public interface IGame : IObservable<Game>
    {
        public IContext GetContext();
        public void TransitionState(State state);
        public SudokuWrapper? SudokuWrapper { get; set; }
        public Board Board { get; }
        public void SelectCell(Position position);
        public void EnterValue(string value);
        public void Solve();
        public void ValidateSudoku(bool update = true);
    }
}