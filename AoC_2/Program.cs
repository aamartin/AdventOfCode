using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var count = 0;
            var answers = new List<string>();
            foreach(var line in input)
            {
                //gsggsggvgggpgrgx
                var parts = line.Split('-', ' ', ':');
                var pw = parts[4];
                var thing = char.Parse(parts[2]);
                var low = int.Parse(parts[0]);
                var high = int.Parse(parts[1]);
                var intcount = 0;
                var chars = pw.ToCharArray();
                if((chars[low-1] == thing || chars[high-1] == thing) && !(chars[low - 1] == thing && chars[high - 1] == thing))
                {
                    count++;
                }

            }
            Console.WriteLine(count);
        }
    }
}
