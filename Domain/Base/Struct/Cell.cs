using Domain.Base.Enums;

namespace Domain.Base.Struct
{
    public struct Cell
    {
        public Cell(char width, int height)
        {
            Width = width;
            Height = height;
            Colour = default;
            Checker = default;
        }
        public Cell(CellColour colour, CellPlace checker, char width, int height)
        {
            Colour = colour;
            Checker = checker;
            Width = width;
            Height = height;
        }
        public CellColour Colour { get; private set; }
        public CellPlace Checker { get; private set; }
        public char Width { get; private set; }
        public int Height { get; private set; }
        public void UpdateCell(CellPlace checker)
            => Checker = checker;
        public bool IsWhiteChecker
            => Colour == CellColour.White;

        public override string ToString()
        {
            return string.Format("Height - {0}, width - {1}, cell color - {2}, cell place - {3}", Height, Width, Colour, Checker);
        }
        public static bool operator ==(Cell cell1, Cell cell2)
        {
            return cell1.Width == cell1.Width && cell2.Height == cell2.Height;
        }
        public static bool operator !=(Cell cell1, Cell cell2)
        {
            return !(cell1 == cell2);
        }

        public static bool operator ==(Cell cell, BoardLocation location)
        {
            return cell.Width == location.Width && cell.Height == location.Height;
        }
        public static bool operator !=(Cell cell, BoardLocation location)
        {
            return !(cell == location);
        }

        public static bool operator ==(BoardLocation location, Cell cell)
        {
            return cell.Width == location.Width && cell.Height == location.Height;
        }
        public static bool operator !=(BoardLocation location, Cell cell)
        {
            return !(cell == location);
        }

    }

}
