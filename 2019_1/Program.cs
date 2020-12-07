using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            double total = 0;
            foreach (var line in lines)
            {
                var fuel = Math.Floor(double.Parse(line) / 3) - 2;
                var fuelfuel = calcFuel(fuel, 0);
                total = total + fuel + fuelfuel;
            }

            Console.WriteLine(total);
        }

        private static double calcFuel(double fuel, double total)
        {
            var newFuel = Math.Floor(fuel / 3) - 2;
            if (newFuel <= 0) return total;
            else
                return calcFuel(newFuel, total + newFuel);
        }
    }
}
