using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AoC_13
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var time = double.Parse(lines[0]);
            var buses = lines[1].Split(',').Where(b => !String.Equals(b, "x"));
            var answer = double.MaxValue;
            double fastestBus = 0;
            foreach (var bus_str in buses)
            {
                var bus = double.Parse(bus_str);
                var nextStop = Math.Ceiling(time / bus) * bus;
                var diff = nextStop - time;
                if (diff < answer) { answer = diff; fastestBus = bus; }
            }

            Console.WriteLine(fastestBus * answer);

            buses = lines[1].Split(',');
            long et = EarliestTime(buses);
            Console.WriteLine(et);
        }

        private static long EarliestTime(IEnumerable<string> buses_str)
        {
            var busses = new List<KeyValuePair<long, long>>();
            for (long i = 0; i < buses_str.Count(); i++)
            {
                var curBus = buses_str.ElementAt((int)i);
                if (!curBus.Equals("x"))
                {
                    busses.Add(new KeyValuePair<long, long>(long.Parse(curBus), i));
                }
            }
            busses = busses.OrderByDescending(bus => bus.Key).ToList();

            var currentBusses = new List<KeyValuePair<long,long>>();

            var timestamp = busses.First().Key - busses.First().Value;
            var interval = busses.First().Key;
            for (var busIndex = 0; busIndex < busses.Count; busIndex++)
            {
                currentBusses.Add(new KeyValuePair<long, long>(busses[busIndex].Key, busses[busIndex].Value));

                while (currentBusses.Any(t => (timestamp + t.Value) % t.Key != 0))
                {
                    timestamp += interval;
                }

                interval = currentBusses.Select(t => t.Key).Aggregate(LCM);
            }

            return timestamp;            
        }

        static long GCD(long[] numbers)
        {
            return numbers.Aggregate(GCD);
        }

        static long GCD(long a, long b)
        {
            return b == 0 ? a : GCD(b, a % b);
        }

        public static long LCM(long a, long b)
        {
            return (a / GCD(a, b)) * b;
        }
    }
}
