namespace Domain.Extension
{
    public class Randomizer
    {
        private static Random random = new Random();
        public static T RandomFromArray<T>(params T[] arr)
        {
            var index = random.Next(0, arr.Length);
            return arr[index];
        }
        public static int RandomInt()
            => random.Next();
    }
}
