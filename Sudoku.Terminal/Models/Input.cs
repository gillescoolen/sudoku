using System;

namespace Sudoku.Terminal.Models
{
    public class Input
    {
        public int Character { get; }
        public Action Action { get; }

        public Input(int character, Action action)
        {
            Character = character;
            Action = action;
        }
    }
}