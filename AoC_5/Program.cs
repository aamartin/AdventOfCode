using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_5
{
    class Node
    {
        public Node Front { get; set; }
        public Node Back { get; set; } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var asses = new List<thing>();
            foreach (var line in lines)
            {
                int rows = 127;
                double lower = 0;
                double upper = 127;
                double slower = 0;
                double supper = 7;
                var parts = line.ToCharArray();
                var cnt = 0;
                double row = 0;
                double seat = 0;
                foreach (var part in parts)
                {
                    var half = Math.Floor((upper - lower) / 2);
                    var shalf = Math.Floor((supper - slower) / 2);
                    if (part == 'F')
                    {
                        upper = lower + half;
                        if (cnt == 6)
                        {
                            row = lower;
                        }
                    }
                    else if (part == 'B')
                    {
                        lower = upper - half;
                        if(cnt == 6)
                        {
                            row = upper;
                        }
                    }
                    else if (part == 'L')
                    {
                        
                        supper = slower + shalf;
                        if (cnt == 9)
                        {
                            seat = slower;
                        }
                    }
                    else if (part == 'R')
                    {
                        slower = supper - shalf;
                        if (cnt == 9)
                        {
                            seat = supper;
                        }
                    }
                    cnt++;
                }

                var assignment = new thing() { row = row, column = seat, pass = line};
                asses.Add(assignment);
            }
            double high = 0;
            double low = double.MaxValue;
            thing highass = null;
            foreach (var ass in asses)
            {
                if(ass.seatId < low) { low = ass.seatId; }
                if (ass.seatId > high) { high = ass.seatId; highass = ass; }
            }
            Console.WriteLine(high);
            
            for (double i = low; i < high; i++)
            {
                bool found = false;
                foreach(var ass in asses)
                {
                    if(ass.seatId == i) { found = true; break; }
                }
                if(!found) {
                    Console.WriteLine(i);
                }
            }
        }
    }

    class thing
    {
        public double row { get; set; }
        public double column { get; set; }
        public double seatId { get { return (row * 8) + column; } }
        public string pass { get; set; }
    }
}
