using WTS.Services.Interfaces;

namespace WTS.Services
{
    /// <summary>
    /// Manages a collection of items of type T.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public class ListManager<T> : IListManager<T>
    {
        private List<T> _items = new List<T>();

        /// <summary>
        /// Returns the count of items in the list.
        /// </summary>
        public int Count => _items.Count;

        /// <summary>
        /// Adds a new item to the collection.
        /// </summary>
        /// <param name="item">The item to add. Cannot be null.</param>
        /// <returns>True if the item is successfully added, otherwise false.</returns>
        public bool Add(T item)
        {
            if (item is null) { return false; }
            _items.Add(item);
            return true;
        }

        /// <summary>
        /// Retrieves an item at the specified index.
        /// </summary>
        /// <param name="index">Index of the item to retrieve.</param>
        /// <returns>The item at the specified index.</returns>
        public T GetItemAt(int index)
        {
            if (!IsIndexInRange(index)) throw new IndexOutOfRangeException($"Index {index} is out of range. It must be between 0 and {_items.Count - 1}");
            return _items[index];
        }

        /// <summary>
        /// Replaces an item at the specified index with a new item.
        /// </summary>
        /// <param name="index">The index of the item to replace.</param>
        /// <param name="newItem">The new item to insert. Cannot be null.</param>
        /// <returns>True if the item was replaced successfully, otherwise false.</returns>
        public bool Replace(int index, T newItem)
        {
            if (!IsIndexInRange(index) || newItem is null) { return false; }
            _items[index] = newItem;
            return true;
        }

        public bool Delete(T item)
        {
            if (item is null || !_items.Contains(item)) { return false; }
            _items.Remove(item);
            return true;
        }

        /// <summary>
        /// Removes an item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <returns>True if the item was removed successfully, otherwise false.</returns>
        public bool DeleteAt(int index)
        {
            if (!IsIndexInRange(index)) { return false; }
            _items.RemoveAt(index);
            return true;
        }

        /// <summary>
        /// Removes all items in the list.
        /// </summary>
        public void DeleteAll()
        {
            _items.Clear();
        }

        /// <summary>
        /// Sorts the itams in the list according to the specified comparer.
        /// </summary>
        /// <param name="comparer">Comparer for the list to be sorted against.</param>
        /// <exception cref="ArgumentNullException">Provided comparer cannot be null.</exception>
        public void SortItems(IComparer<T> comparer)
        {
            if (comparer is null) throw new ArgumentNullException(nameof(comparer));
            _items.Sort(comparer);
        }

        /// <summary>
        /// Reverses the sort order of the list.
        /// </summary>
        public void ReverseItems()
        {
            _items.Reverse();
        }

        /// <summary>
        /// Returns an array of strings where each string represents an item in the collection.
        /// </summary>
        /// <returns>An array of strings representing the items.</returns>
        public string[] ToStringArray() => _items.Select(item => item.ToString()).ToArray();

        /// <summary>
        /// Returns a list of strings where each string represents an item in the collection.
        /// </summary>
        /// <returns>A list of strings representing the items.</returns>
        public List<string> ToStringList() => _items.Select(item => item.ToString()).ToList();

        /// <summary>
        /// Returns a copy of the collection.
        /// </summary>
        /// <returns>A list of the collection.</returns>
        public List<T> CopyList() => _items.ToList();

        /// <summary>
        /// Checks if the given index is within the range of the collection.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if the index is out of range, otherwise false.</returns>
        public bool IsIndexInRange(int index) => index >= 0 && index < _items.Count;
    }
}