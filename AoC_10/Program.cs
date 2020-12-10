using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_10
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var intlines = new List<int>();
            var onejot = 0;
            var threejot = 0;

            intlines.Add(0);
            foreach (var line in lines)
            {
                intlines.Add(int.Parse(line));
            }

            var maxjolts = intlines.Max();
            intlines.Add(maxjolts + 3);
            var sortedlines = intlines.ToArray();
            Array.Sort(sortedlines);
            for (int i = 1; i < sortedlines.Length; i++)
            {
                if (sortedlines[i] - sortedlines[i - 1] == 1) onejot++;
                else if (sortedlines[i] - sortedlines[i - 1] == 3) threejot++;
            }

            Console.WriteLine(onejot * threejot);

            //Part 2
            double total = 1;
            var dict = new Dictionary<int, double>();
            int previous = 0;
            for (int i = 1; i < sortedlines.Count() - 1; i++)
            {
                var current = sortedlines[i];
                var next = sortedlines[i + 1];
                if (current - previous == 1 && next - previous < 3)  // we are going to have some skipping
                {
                    // add all the previous branches to this one
                    double newPaths = 1;
                    foreach (var skippedNum in dict.Keys)
                    {
                        // We have already taken into account the paths represented by the previous 
                        // if it is contiguious sequence.
                        if (skippedNum == previous && dict.TryGetValue(skippedNum - 1, out var value))
                        {
                            newPaths = newPaths + dict[skippedNum] - value;
                        }
                        else
                        {
                            newPaths = newPaths + dict[skippedNum];
                        }
                    }
                    total = total + newPaths;
                    dict[current] = newPaths;
                }

                previous = current;
            }

            Console.WriteLine(total);
        }
    }
}
