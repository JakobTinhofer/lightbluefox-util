using System;
using System.Collections.Generic;
using System.Text;

namespace LightBlueFox.Util
{
    /// <summary>
    /// The internal type for <see cref="ProbabilityList{T}"/>
    /// </summary>
    public class ProbabilityEntry
    {
        /// <summary>
        /// The item contained. This is either of type T (the generic parameter to the <see cref="ProbabilityList{T}"/> owning this item) or of type <see cref="ProbabilityList{T}"/>
        /// </summary>
        public object Item { get; internal set; }
        /// <summary>
        /// The probability of the item being picked. NOTE THAT THIS IS NOT IN PERCENT! Just like percent is from 0 to 100, this is from 0 to sum of all probabilities in list.
        /// </summary>
        public int Probability { get; internal set; }
        internal int AccumulatedProbability { get; set; }
        internal ProbabilityEntry(object item, int prob, int accumulatedProb) { Item = item; Probability = prob; AccumulatedProbability = accumulatedProb; }
    }

}
