using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_6
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var total = 0;
            var grandtotal = 0;
            var totals = new List<int>();
            var peeps = 0;
            Dictionary<char, int> qs = new Dictionary<char, int>();
            foreach(var line in lines)
            {
                peeps = peeps + 1;
                if(String.IsNullOrEmpty(line))
                {
                    foreach(var answer in qs.Keys)
                    {
                        if (qs[answer] == peeps-1)
                        {
                            grandtotal++;
                        }
                    }

                    var temp = qs.ToList();
                    totals.Add(temp.Count);
                    //grandtotal = grandtotal + temp.Count;
                    qs.Clear();
                    total = 0;
                    peeps = 0;
                    continue;
                }
                var parts = line.ToCharArray();
                foreach(var part in parts)
                {
                    if(qs.ContainsKey(part))
                    {
                        qs[part]++;
                    }
                    else
                    {
                        qs[part] = 1;
                    }
                }

            }

            var blah = qs.ToList();
            foreach (var answer in qs.Keys)
            {
                if (qs[answer] == peeps)
                {
                    grandtotal++;
                }
            }
        }
    }
}
