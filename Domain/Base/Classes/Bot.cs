using Domain.Base.Enums;
using Domain.Extension;

namespace Domain.Base.Classes
{
    public class Bot : CheckersPlayer
    {
        public Bot(int id, CellColor checkerColor) : base(id, checkerColor)
        {
        }

        public int ChooseMove(int[] moves)
        {
            return Randomizer.RandomFromArray(moves);
        }
    }
}
