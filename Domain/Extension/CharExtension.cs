namespace Domain.Extension
{
    public static class CharExtension
    {
        public static bool IsLetter(this char @char)
            => char.IsLetter(@char);
        public static bool IsUpper(this char @char)
            => char.IsUpper(@char);
    }
}
