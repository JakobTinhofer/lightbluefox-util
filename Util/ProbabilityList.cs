using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Util
{ 
    /// <summary>
    /// A list of items and their probabilities. Allows you to add and remove items at all time and then get a random one weighted by their exact probability.
    /// </summary>
    /// <typeparam name="T">The type of the item in the List</typeparam>
    public class ProbabilityList<T> : IEnumerable<ProbabilityEntry>
    {
        #region Private Members
        private int _total;
        private List<ProbabilityEntry> entries = new List<ProbabilityEntry>();
        private Random randomGen = new Random();
        #endregion

        #region Methods
        /// <summary>
        /// Returns a random entry of type T from the entries in the list, weighted by their probability. 
        /// </summary>
        /// <returns></returns>
        public T GetRandomItem()
        {
            SortEntries();
            int res = randomGen.Next(0, _total);
            foreach (var e in entries)
            {
                if (e.AccumulatedProbability > res)
                {
                    if (e.Item.GetType() == typeof(ProbabilityList<T>))
                        return ((ProbabilityList<T>)e.Item).GetRandomItem();
                    return (T)e.Item;
                }
            }
            throw new InvalidOperationException("Internal Error in the probability distribution of list. No Value has been found with accumulated probability greater than random value of " + res + ".");
        }

        /// <summary>
        /// Sorts all the entries according to their accumulated probability.
        /// </summary>
        private void SortEntries() => entries.Sort(delegate (ProbabilityEntry e1, ProbabilityEntry e2) { return e1.AccumulatedProbability.CompareTo(e2.AccumulatedProbability); });
        #endregion


        #region Add / Remove Entries
        /// <summary>
        /// Adds a new entry to the list. This will also affect the probability of all other entries.
        /// (since the chance of an entry being picked is probability / total_probabilities)
        /// </summary>
        /// <param name="item">The item you want to add. Duplicates are allowed.</param>
        /// <param name="probability">The probability of the item. Keep in mind, the probability of an item being picked is probability / sum of probabilities</param>
        public void Add(T item, int probability) => _add(item, probability);
        /// <summary>
        /// Adds a new entry to the list. This will also affect the probability of all other entries.
        /// (since the chance of an entry being picked is probability / total_probabilities)
        /// This entry is a list of itsself, so when this would be picked by <see cref="GetRandomItem"/>, <see cref="GetRandomItem"/> is also called on this list.
        /// </summary>
        /// <param name="list">The list you want to add. Should be of length > 0.</param>
        /// <param name="probability">The probability of the list. Keep in mind, the probability of a list being picked is probability / sum of probabilities</param>
        public void Add(ProbabilityList<T> list, int probability) => _add(list, probability);
        private void _add(Object o, int prob)
        {
            _total += prob;
            entries.Add(new ProbabilityEntry(o, prob, _total));
        }
        #endregion


        #region Inherited from IEnumerable
        /// <summary>
        /// Inherited from <see cref="IEnumerable"/>
        /// </summary>
        public IEnumerator<ProbabilityEntry> GetEnumerator() => entries.GetEnumerator();
        /// <summary>
        /// Inherited from <see cref="IEnumerable"/>
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        #endregion
    }
}
