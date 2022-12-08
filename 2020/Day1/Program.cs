using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            foreach(var line in input)
            {
                foreach(var line1 in input)
                {
                    foreach (var line2 in input)
                    {
                        int one = int.Parse(line);
                        int two = int.Parse(line1);
                        int three = int.Parse(line2);
                        if (one + two + three == 2020)
                        {
                            Console.WriteLine(one * two * three);
                        }
                    }
                }
            }
        }
    }
}
