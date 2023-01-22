namespace Domain.Base.Struct
{
    public struct BoardLocation
    {
        public BoardLocation() { }

        public BoardLocation(char width, int height)
        {
            Width = width;
            Height = height;
        }

        public char Width { get; set; }
        public int Height { get; set; }
    }
}
