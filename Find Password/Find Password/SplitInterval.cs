using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Find_Password
{
    public class SplitInterval
    {
        private long start;
        private long stop;
        private long numberOfIntervals;
        private List<KeyValuePair<long, long>> intervals = new List<KeyValuePair<long, long>>();

        public SplitInterval(long start, long stop, long numberOfIntervals)
        {
            this.start = start;
            this.stop = stop;
            this.numberOfIntervals = numberOfIntervals;
        }

        private void CalculatesNumberOfElementsForInterval(ref long numberOfElementsInInterval, 
            ref long numberOfAdditionalElements)
        {
            long numberOfElements = stop - start + 1;
            numberOfElementsInInterval = numberOfElements / numberOfIntervals;
            numberOfAdditionalElements = numberOfElements % numberOfIntervals;
        }

        private void IncreaseCurrentNumber(ref long endInterval, ref long numberOfAdditionalElements, long numberOfElementsInInterval)
        {
            endInterval += numberOfAdditionalElements-- > 0 ? numberOfElementsInInterval : numberOfElementsInInterval -1;
        }

        public void Split()
        {
            intervals.Clear();

            long endInterval = start;
            long startInterval = start;
            long numberOfElementsInInterval = 0;
            long numberOfAdditionalElements = 0;

            CalculatesNumberOfElementsForInterval(ref numberOfElementsInInterval, ref numberOfAdditionalElements);

            long numberOfIntervalsAux = numberOfIntervals;
            while (numberOfIntervalsAux-- > 0)
            {
                IncreaseCurrentNumber(ref endInterval, ref numberOfAdditionalElements, numberOfElementsInInterval);
                intervals.Add(new KeyValuePair<long, long>(startInterval, endInterval));

                ++endInterval;
                startInterval = endInterval;
            }
        } 

        public List<KeyValuePair<long, long>> Intervals
        {
            get { return intervals; }
        }
    }
}
