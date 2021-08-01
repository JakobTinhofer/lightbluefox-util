using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class ProbabilityList<T> : IEnumerable<ProbabilityEntry>
    {
        private int _total;
        private List<ProbabilityEntry> entries = new List<ProbabilityEntry>();
        private Random randomGen = new Random();

        

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

        private void SortEntries()
        {
            entries.Sort(delegate (ProbabilityEntry e1, ProbabilityEntry e2) { return e1.AccumulatedProbability.CompareTo(e2.AccumulatedProbability); });
        }

        public void Add(T item, int probability) => _add(item, probability);
        public void Add(ProbabilityList<T> list, int probability) => _add(list, probability);
        private void _add(Object o, int prob)
        {
            _total += prob;
            entries.Add(new ProbabilityEntry(o, prob, _total));
        }

        public IEnumerator<ProbabilityEntry> GetEnumerator()
        {
            return entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
