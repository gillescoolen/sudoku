using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sudoku.Domain.Models.Interfaces;
using Sudoku.Domain.Utilities;

namespace Sudoku.Domain.Models
{
    public class SquareLeaf : IComponent
    {
        private bool isLocked;
        public bool IsLocked
        {
            get => isLocked;
            private init => isLocked = value;
        }

        public bool IsValid { private get; set; } = true;
        public bool IsSelected { get; private set; }
        private string value;
        private string helpValue = "0";
        public Coordinate Coordinate { get; }

        public string Value
        {
            get => value;
            set
            {
                if (IsLocked) return;
                this.value = value;
                IsValid = true;
                helpValue = "0";
            }
        }

        public string HelpValue
        {
            get => helpValue;
            set
            {
                if (IsLocked || !Value.Equals("0")) return;
                helpValue = value;
                IsValid = true;
            }
        }

        public SquareLeaf(bool isLocked, string value, Coordinate coordinate)
        {
            IsLocked = isLocked;
            this.value = value;
            Coordinate = coordinate;
        }

        public SquareLeaf(bool isLocked, Coordinate coordinate)
        {
            IsLocked = isLocked;
            value = "";
            Coordinate = coordinate;
        }

        public void ToggleSelect()
        {
            IsSelected = !IsSelected;
        }

        public bool Valid(State state, bool setValid)
        {
            return IsValid;
        }

        public bool Valid()
        {
            return IsValid;
        }

        public bool IsComposite()
        {
            return false;
        }

        public IEnumerable<IComponent> Find(Func<IComponent, bool> finder)
        {
            return GetChildren();
        }

        public bool IsSpacingSquare()
        {
            return IsLocked && value.Length == 0;
        }

        public IEnumerable<IComponent> GetChildren()
        {
            return new ReadOnlyCollection<IComponent>(new List<IComponent>());
        }
    }
}