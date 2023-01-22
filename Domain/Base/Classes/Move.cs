using Domain.Base.Struct;

namespace Domain.Base.Classes
{
    public class Move
    {
        public Move(LinkedList<Cell> path)
        {
            Path = path;
            Id = GetHash(path);
        }
        public int Id { get; private set; }
        public LinkedList<Cell> Path { get; private set; }

        private int GetHash(LinkedList<Cell> path)
        {
            int hash = 19;
            foreach (var cell in path)
            {
                hash = hash * 31 + cell.GetHashCode();
            }
            return hash;
        }
    }
}
