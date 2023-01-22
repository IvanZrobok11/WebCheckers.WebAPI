using Domain.Base.Enums;

namespace Domain.Base.Struct
{
    public struct CheckersPlayer
    {
        public CheckersPlayer(int id, CellColour checkerColour)
        {
            Id = id;
            CheckerColour = checkerColour;
        }
        public int Id { get; private set; }
        public CellColour CheckerColour { get; set; }
    }

}
