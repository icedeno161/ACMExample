using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.Library
{
    public class Builder
    {
        /// <summary>
        /// Returns an Enumerable sequence of integers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<int> BuildIntegerSequence() => Enumerable.Range(0, 10)
                                                            .Select(i => 5 + (10 * i));

        /// <summary>
        /// Returns a random Enumerable sequence of numbers.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> BuildStringSequence()
        {
            Random rand = new Random();

            return Enumerable.Range(0, 10)
                    .Select(i => ((char)('A' + rand.Next(0, 26))).ToString());
        }

        public IEnumerable<int> CompareSequences()
        {
            var seq1 = Enumerable.Range(0, 10);
            var seq2 = Enumerable.Range(0, 10)
                                .Select(i => i * i);

            return seq1.Union(seq2);
        }
    }
}
