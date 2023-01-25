using Domain.Base.Enums;

namespace Domain.Base.Classes
{
    public class CheckersPlayer
    {
        public CheckersPlayer(int id, CellColour checkerColour)
        {
            Id = id;
            Colour = checkerColour;
        }
        public int Id { get; private set; }
        public CellColour Colour { get; set; }
    }

}
