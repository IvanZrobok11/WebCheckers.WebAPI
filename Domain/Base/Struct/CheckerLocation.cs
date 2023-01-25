namespace Domain.Base.Struct
{
    public struct CheckerLocation
    {
        public CheckerLocation() { }

        public CheckerLocation(char width, int height)
        {
            Width = width;
            Height = height;
        }

        public char Width { get; set; }
        public int Height { get; set; }
    }
}
