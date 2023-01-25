using Domain.Base.Enums;
using Domain.Base.Struct;
using Domain.Extension;

namespace Domain.Base.Classes
{
    public class Board
    {
        public const int INDEX_ASCII_FOR_FIRST_CAPITAL_LETTTER = 65;
        public const bool WHITE_IS_DOWN = true;
        public static int BoardLenght { get; } = 8;

        internal readonly Cell[,] fields;

        public Board()
        {
            fields = new Cell[BoardLenght, BoardLenght];
            FillField();
            ArrangeCheckers();
        }
        private Board(Cell[,] fields)
        {
            this.fields = fields;
        }

        public Board Copy()
        {
            var copyFields = new Cell[8, 8];
            Array.Copy(this.fields, copyFields, copyFields.Length);
            var copy = new Board(copyFields);
            return copy;
        }

        #region
        public void TraversalField(Action<int, int> action, Action<int> betweenFor = null)
        {
            for (int height = 0; height < fields.GetLength(0); height++)
            {
                for (int width = 0; width < fields.GetLength(1); width++)
                    action.Invoke(height, width);

                if (betweenFor != null)
                    betweenFor.Invoke(height);
            }
        }
        public void RevertTraversalField(Action<int, int> action, Action<int> betweenFor = null)
        {
            for (int height = fields.GetLength(0) - 1; height >= 0; height--)
            {
                for (int width = 0; width < fields.GetLength(1); width++)
                    action.Invoke(height, width);

                if (betweenFor != null)
                    betweenFor.Invoke(height);
            }
        }
        #endregion

        private void FillField()
        {
            TraversalField((height, width) =>
            {
                var colour = (height + width) % 2 == 0 ? CellColour.Black : CellColour.White;
                var widthChar = IndexToWidth(width);
                fields[height, width] = new Cell(colour, CellPlace.Empty, widthChar, IndexToHeight(height));
            });
        }
        private void ArrangeCheckers()
        {
            TraversalField((height, width) =>
            {
                var cell = fields[height, width];
                if (cell.Colour == CellColour.White) return;

                if (height < 3)
                    fields[height, width].UpdateCell(CellPlace.WhiteChecker);
                else if (height > 4)
                    fields[height, width].UpdateCell(CellPlace.BlackChecker);
            });
        }

        public static bool ValidateCoordinate(char width, int height)
        {
            var indexHeight = HeightToIndex(height);
            var indexWidth = WidthToIndex(width);
            return indexHeight < BoardLenght && indexHeight >= 0 && indexWidth < BoardLenght && indexWidth >= 0;
        }

        public bool TryGetCell(CheckerLocation location, out Cell? cell)
           => TryGetCell(location.Width, location.Height, out cell);
        public bool TryGetCell(char width, int height, out Cell? cell)
        {
            if (!ValidateCoordinate(width, height))
            {
                cell = null;
                return false;
            }
            var indexHeight = HeightToIndex(height);
            var indexWidth = WidthToIndex(width);
            cell = GetCellByIndex(indexHeight, indexWidth);
            return true;
        }

        public Cell GetCell(CheckerLocation location)
            => GetCell(location.Width, location.Height);
        public Cell GetCell(char width, int height)
        {
            if (!TryGetCell(width, height, out Cell? cell))
                throw new ArgumentException(string.Format("This impossible find cell with coordinate: height [{0}] or width [{1}] value", height, width));
            return cell!.Value;
        }
        public Cell GetCellByIndex(int height, int width) => fields[height, width];

        private static int HeightToIndex(int height) => height - 1;
        private static int WidthToIndex(char width) => (int)width - INDEX_ASCII_FOR_FIRST_CAPITAL_LETTTER;
        private static char IndexToWidth(int index) => (char)(index + INDEX_ASCII_FOR_FIRST_CAPITAL_LETTTER);
        private static int IndexToHeight(int height) => height + 1;


        /// <summary>
        /// returns all incident nodes(cells)
        /// </summary>
        internal List<Cell> GetAllAdjacentAccrossCells(Cell cell)
        {
            var accrossCells = new List<Cell>(4);
            Cell? nextCell;
            if (TryGetCell((char)(cell.Width - 1), cell.Height + 1, out nextCell))
                accrossCells.Add(nextCell!.Value);
            if (TryGetCell((char)(cell.Width + 1), cell.Height + 1, out nextCell))
                accrossCells.Add(nextCell!.Value);
            if (TryGetCell((char)(cell.Width + 1), cell.Height - 1, out nextCell))
                accrossCells.Add(nextCell!.Value);
            if (TryGetCell((char)(cell.Width - 1), cell.Height - 1, out nextCell))
                accrossCells.Add(nextCell!.Value);

            return accrossCells;
        }

        public static bool OnSameDiagonale(Cell cell1, Cell cell2)
            => OnSameDiagonale(WidthToIndex(cell1.Width), HeightToIndex(cell1.Height), WidthToIndex(cell2.Width), HeightToIndex(cell2.Height));
        private static bool OnSameDiagonale(int width1, int height1, int width2, int height2)
            => width1.GetBiggest(height1) - width1.GetLower(height1) == width2.GetBiggest(height2) - width2.GetLower(height2);

        public static bool OnEndLine(Cell cell)
        {
            if (cell.Checker.IsBlack())
            {
                return cell.Height == 1;
            }
            else if (cell.Checker.IsWhite())
            {
                return cell.Height == 8;
            }
            else
            {
                throw new ArgumentException($"Cell with checker {cell.Checker} cannot be on end line!");
            }
        }

        //public bool CheckerCanMove(Cell cell)
        //{
        //    if (GetCell(to.Width, to.Height).Checker != CellPlace.Empty)
        //    {
        //        return false;
        //    }
        //    if (!MoveIsForward(with, to) && with.Checker != Queen)
        //    {
        //        return false;
        //    }

        //    //TODO:

        //    return true;
        //}
    }
}
