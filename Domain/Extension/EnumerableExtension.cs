namespace Domain.Extension
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> SkipWhere<T>(this IEnumerable<T> items, Func<T, bool> predicate)
        {
            foreach (var item in items)
            {
                if (predicate(item))
                {
                    continue;
                }
                yield return item;
            }
        }
    }
}
