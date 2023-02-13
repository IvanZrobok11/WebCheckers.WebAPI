using Domain.Base.Enums;

namespace Domain.Base.Classes
{
    public class CheckersPlayer
    {
        public CheckersPlayer(int id, CellColor checkerColor)
        {
            Id = id;
            Color = checkerColor;
        }
        public int Id { get; private set; }
        public CellColor Color { get; set; }
    }

}
