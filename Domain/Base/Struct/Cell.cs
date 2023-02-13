using Domain.Base.Enums;

namespace Domain.Base.Struct
{
    public struct Cell
    {
        public Cell(CheckerLocation location)
        {
            Width = location.Width;
            Height = location.Height;
            Color = default;
            Checker = default;
        }
        public Cell(char width, int height)
        {
            Width = width;
            Height = height;
            Color = default;
            Checker = default;
        }
        public Cell(CellColor colour, CellPlace checker, char width, int height)
        {
            Color = colour;
            Checker = checker;
            Width = width;
            Height = height;
        }
        public Cell(CellColor colour, CellPlace checker, CheckerLocation location)
        {
            Color = colour;
            Checker = checker;
            Width = location.Width;
            Height = location.Height;
        }
        public CellColor Color { get; private set; }
        public CellPlace Checker { get; private set; }
        public char Width { get; private set; }
        public int Height { get; private set; }
        public void UpdateCell(CellPlace checker)
            => Checker = checker;
        public bool IsWhiteChecker
            => Color == CellColor.White;

        public override string ToString()
        {
            return string.Format("Width-({0}), Height-({1}), cell color-({2}), cell place-({3})", Width, Height, Color, Checker);
        }

        public bool LocationEquels(Cell location)
            => Width == location.Width && Height == location.Height;
        public override bool Equals(object? obj)
        {
            return obj is Cell cell &&
                   Color == cell.Color &&
                   Checker == cell.Checker &&
                   Width == cell.Width &&
                   Height == cell.Height;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Color, Checker, Width, Height);
        }

        public static bool operator ==(Cell cell1, Cell cell2)
        {
            return cell1.Equals(cell2);
        }
        public static bool operator !=(Cell cell1, Cell cell2)
        {
            return !(cell1 == cell2);
        }

        public static bool operator ==(Cell cell, CheckerLocation location)
        {
            return cell.Width == location.Width && cell.Height == location.Height;
        }
        public static bool operator !=(Cell cell, CheckerLocation location)
        {
            return !(cell == location);
        }

        public static bool operator ==(CheckerLocation location, Cell cell)
        {
            return cell.Width == location.Width && cell.Height == location.Height;
        }
        public static bool operator !=(CheckerLocation location, Cell cell)
        {
            return !(cell == location);
        }
    }

}
