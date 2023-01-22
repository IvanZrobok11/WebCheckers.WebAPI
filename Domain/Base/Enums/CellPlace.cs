namespace Domain.Base.Enums
{
    [Flags]
    public enum CellPlace
    {
        Empty = 1,        // 0b_0001,
        BlackChecker = 2, // 0b_0001,
        WhiteChecker = 4, // 0b_0010,
        BlackQueen = 8,  // 0b_0100,
        WhiteQueen = 16,  // 0b_1000,
    }
}
