using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_3
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var over = 0;
            var down = 1;
            var trees = 0;
            var total = -1;
            var skipped = 1;
            foreach(var line in lines)
            {
                total++;
                if (total == 0 ) continue;
                if (total % 2 != 0) continue;
                over++;
                var chars = line.ToCharArray();
                char at = chars[(over) % line.Length];
                if (at == '#')
                {
                    trees++;
                }
            }
            Console.WriteLine(trees);
        }
    }
}
