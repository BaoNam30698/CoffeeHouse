namespace CoffeeHouse.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static void AddItems<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            foreach (T item in items)
            {
                collection.Add(item);
            }
        }
    }
}
