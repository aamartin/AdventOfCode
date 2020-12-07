using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2019_2
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            foreach (var line in lines)
            {
                for (int noun = 0; noun < 99; noun++)
                {
                    for (int verb = 0; verb < 99; verb++)
                    {
                        var parts = line.Split(',');
                        parts[1] = $"{noun}";
                        parts[2] = $"{verb}";
                        for (int i = 0; i < parts.Length; i = i + 4)
                        {
                            var opcode = int.Parse(parts[i]);
                            if (opcode == 99) { break; }
                            var one = int.Parse(parts[i + 1]);
                            var two = int.Parse(parts[i + 2]);
                            var output = int.Parse(parts[i + 3]);
                            if (output > parts.Length || one > parts.Length || two > parts.Length)
                            { break; }
                            var vone = int.Parse(parts[one]);
                            var vtwo = int.Parse(parts[two]);
                            var value = vone + vtwo;
                            //if (opcode == 1) { parts[output] = $"{vone + }"; }
                            if (opcode == 2) { value = vone * vtwo; }
                            parts[output] = $"{value}";
                        }
                        if (int.Parse(parts[0]) == 19690720)
                        {
                            Console.WriteLine($"{(noun * 100) + verb}");
                        }
                    }
                }
            }
        }
    }
}
