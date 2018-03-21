using System;
using System.Collections;
using System.Collections.Generic;

namespace Metasense.Infrastructure
{
    /// <summary>
    /// A sorted list of types that are <see cref="IComparable"/>
    /// </summary>
    /// <remarks>
    /// This should have been part of the .NET framework!!
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    /// TODO : This is fucking broken! fix it
    public class SortedList<T> : IList<T> where T : IComparable<T>
    {

        /// <summary>
        /// The simple internal list.
        /// </summary>
        private List<T> internalList;

        /// <summary>
        /// .ctor
        /// </summary>
        public SortedList()
        {
            internalList = new List<T>();
        }

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
        {
            return internalList.GetEnumerator();
        }

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Add an item to its appropriate position int the array. Time complexity is O(log(n))
        /// </summary>
        /// <param name="item"></param>
        public void Add(T item)
        {
            TryFind(item, out var index);
            Insert(index, item);
        }

        /// <summary>
        /// Adds a series of items to their appropriate position in the array. Time complexity is O(n log(n))
        /// </summary>
        /// <param name="items"></param>
        public void AddRange(IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                Add(item);
            }
        }

        /// <inheritdoc />
        public void Clear()
        {
            internalList.Clear();
        }

        /// <summary>
        /// Check to see if the given item is contained within the array. Time complexity is O(log(n))
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Contains(T item)
        {
            return TryFind(item, out var ignoredIndex).Item1;
        }

        /// <summary>
        /// Checks to see if the there is a value in the list which compares equally to the given item (even if it is not equal to the item)
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns>Returns the index of the comparable item or the index at which the new item would have been inserted</returns>
        public bool ContainsComparableValue(T item, out int index)
        {
            return TryFind(item, out index).Item2;
        }

        /// <inheritdoc />
        public void CopyTo(T[] array, int arrayIndex)
        {
            internalList.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Remove an item from the array and indicate success or failure of the operation. Time complexity is O(log(n))
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Remove(T item)
        {
            var retVal = TryFind(item, out var index).Item1;
            if (retVal)
            {
                RemoveAt(index);
            }

            return retVal;
        }

        /// <inheritdoc />
        public int Count => internalList.Count;

        /// <inheritdoc />
        public bool IsReadOnly => false;

        /// <summary>
        /// Gets the index of the given item. Time complexity is O(log(n))
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int IndexOf(T item)
        {
            return TryFind(item, out var index).Item1 ? index : -1;
        }

        /// <summary>
        /// Insert the item at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <param name="item"></param>
        public void Insert(int index, T item)
        {
            internalList.Insert(index, item);
        }

        /// <summary>
        /// Remove the item at the given idex
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            internalList.RemoveAt(index);
        }

        /// <inheritdoc />
        public T this[int index]
        {
            get => internalList[index];
            set => internalList[index] = value;
        }

        /// <summary>
        /// O(log(n)) for finding item. Performs a binary search
        /// </summary>
        /// <param name="item">The item to find</param>
        /// <param name="index">The index of the found item. If the item wasn't found, this is the index at which the given item must be inserted</param>
        /// <returns>True or False depending on whether or not the given item was found</returns>
        private Tuple<bool, bool> TryFind(T item, out int index)
        {
            var objectFound = false;
            var comparableValueFound = false;
            var currentIndex = internalList.Count / 2;
            var foundSpot = false;
            while (!foundSpot)
            {
                var moreThanPrevious = currentIndex == 0 || item.CompareTo(internalList[currentIndex - 1]) > 0;
                var equalToCurrent = Count > 0 && IsIndexValid(currentIndex) && item.CompareTo(internalList[currentIndex]) == 0;
                var lessThanNext = currentIndex == internalList.Count || item.CompareTo(internalList[currentIndex]) < 0;

                //If at any point, the comparable value is found, set this flag
                if (equalToCurrent)
                {
                    comparableValueFound = true;
                }

                if (moreThanPrevious && lessThanNext || equalToCurrent)
                {
                    objectFound = Count > 0 && IsIndexValid(currentIndex) && item.Equals(internalList[currentIndex]);
                    foundSpot = true;
                }
                else if (moreThanPrevious && !lessThanNext)
                {
                    currentIndex += (internalList.Count - currentIndex + 1) / 2;
                }
                else if (!moreThanPrevious && lessThanNext)
                {
                    currentIndex = currentIndex / 2;
                }
            }

            index = currentIndex;

            return new Tuple<bool, bool>(objectFound, comparableValueFound);
        }

        /// <summary>
        /// Check if the given index is valid
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        private bool IsIndexValid(int index)
        {
            return Count > 0 && index >= 0 && index < Count;
        }
    }
}
