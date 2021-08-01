using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class ProbabilityEntry
    {
        public object Item;
        public int Probability;
        public int AccumulatedProbability;
        public ProbabilityEntry(object item, int prob, int accumulatedProb) { Item = item; Probability = prob; AccumulatedProbability = accumulatedProb; }
    }

}
