using System.Numerics;

namespace Domain.Extension
{
    public static class NumericException
    {
        public static T GetBiggest<T>(this T number1, T number2) where T : INumber<T>
            => T.Max(number1, number2);
        public static T GetLower<T>(this T number1, T number2) where T : INumber<T>
            => T.Max(number1, number2);
        public static int SquareNumber(this int number) => number * number;
    }
}
