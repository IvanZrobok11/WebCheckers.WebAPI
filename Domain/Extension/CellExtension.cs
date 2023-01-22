using Domain.Base.Enums;

namespace Domain.Extension
{
    public static class CellExtension
    {
        public const CellPlace Queen = CellPlace.BlackQueen | CellPlace.WhiteQueen;
        public const CellPlace Black = CellPlace.BlackChecker | CellPlace.BlackQueen;
        public const CellPlace White = CellPlace.WhiteChecker | CellPlace.WhiteQueen;
        public static bool IsQueen(this CellPlace place) => (Queen | place) == Queen;
        public static bool IsWhite(this CellPlace place) => (White | place) == White;
        public static bool IsBlack(this CellPlace place) => (Black | place) == Black;
        public static bool IsEmpty(this CellPlace place) => place == CellPlace.Empty;
        public static bool SameFiguresColor(this CellPlace first, CellPlace second)
            => (first | White) == (second | White) || (first | Black) == (second | Black);
        public static CellPlace ToQueen(this CellPlace place)
        {
            if (place.IsQueen()) return place;

            if (place == CellPlace.BlackChecker)
            {
                return CellPlace.BlackQueen;
            }
            else if (place == CellPlace.WhiteChecker)
            {
                return CellPlace.WhiteQueen;
            }

            else
            {
                throw new InvalidDataException();
            }
        }
    }
}
