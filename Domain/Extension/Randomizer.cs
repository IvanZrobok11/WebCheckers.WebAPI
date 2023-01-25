namespace Domain.Extension
{
    public class Randomizer
    {
        private static Random random = new Random();
        public static T RandomFromArray<T>(params T[] arr)
        {
            var index = random.Next(arr.Length);
            return arr[index];
        }
    }
}
