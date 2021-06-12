using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Sudoku.Domain.Models.Interfaces;

namespace Sudoku.Domain.Models
{
    public class CellLeaf : IComponent
    {
        public bool IsLocked { get; }
        public bool IsValid { private get; set; } = true;
        public bool IsSelected { get; private set; }
        private string value;
        public Position Position { get; }

        public string Value
        {
            get => value;
            set
            {
                if (IsLocked) return;
                this.value = value;
                IsValid = true;
            }
        }

        public CellLeaf(bool isLocked, string value, Position position)
        {
            IsLocked = isLocked;
            this.value = value;
            Position = position;
        }

        public CellLeaf(bool isLocked, Position position)
        {
            IsLocked = isLocked;
            value = "";
            Position = position;
        }

        public void ToggleSelect()
        {
            IsSelected = !IsSelected;
        }

        public bool Valid()
        {
            return IsValid;
        }

        public bool IsComponent()
        {
            return false;
        }

        public IEnumerable<IComponent> Find(Func<IComponent, bool> finder)
        {
            return GetChildren();
        }

        public bool IsSpacingCell()
        {
            return IsLocked && value.Length == 0;
        }

        public IEnumerable<IComponent> GetChildren()
        {
            return new ReadOnlyCollection<IComponent>(new List<IComponent>());
        }
    }
}