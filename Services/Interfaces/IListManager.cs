namespace WTS.Services.Interfaces
{
    /// <summary>
    /// Manages a collection of items of type T.
    /// </summary>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public interface IListManager<T>
    {
        /// <summary>
        /// Returns the count of items in the list.
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Adds a new item to the collection.
        /// </summary>
        /// <param name="item">The item to add. Cannot be null.</param>
        /// <returns>True if the item is successfully added, otherwise false.</returns>
        bool Add(T item);

        /// <summary>
        /// Retrieves an item at the specified index.
        /// </summary>
        /// <param name="index">Index of the item to retrieve.</param>
        /// <returns>The item at the specified index.</returns>
        T GetItemAt(int index);

        /// <summary>
        /// Replaces an item at the specified index with a new item.
        /// </summary>
        /// <param name="index">The index of the item to replace.</param>
        /// <param name="newItem">The new item to insert. Cannot be null.</param>
        /// <returns>True if the item was replaced successfully, otherwise false.</returns>
        bool Replace(int index, T newItem);

        /// <summary>
        /// Removes the item specified.
        /// </summary>
        /// <param name="item">The index of the item to remove.</param>
        /// <returns>True if the item was removed successfully, otherwise false.</returns>
        bool Delete(T item);

        /// <summary>
        /// Removes an item at the specified index.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <returns>True if the item was removed successfully, otherwise false.</returns>
        bool DeleteAt(int index);

        /// <summary>
        /// Removes all items in the list.
        /// </summary>
        void DeleteAll();

        /// <summary>
        /// Sorts the itams in the list according to the specified comparer.
        /// </summary>
        /// <param name="comparer">Comparer for the list to be sorted against.</param>
        void SortItems(IComparer<T> comparer);

        /// <summary>
        /// Reverses the sort order of the list.
        /// </summary>
        public void ReverseItems();

        /// <summary>
        /// Returns an array of strings where each string represents an item in the collection.
        /// </summary>
        /// <returns>An array of strings representing the items.</returns>
        string[] ToStringArray();

        /// <summary>
        /// Returns a list of strings where each string represents an item in the collection.
        /// </summary>
        /// <returns>A list of strings representing the items.</returns>
        List<string> ToStringList();

        /// <summary>
        /// Checks if the given index is within the range of the collection.
        /// </summary>
        /// <param name="index">The index to check.</param>
        /// <returns>True if the index is out of range, otherwise false.</returns>
        bool IsIndexInRange(int index);

        /// <summary>
        /// Returns a copy of the collection.
        /// </summary>
        /// <returns>A list of the collection.</returns>
        List<T> CopyList();
    }
}