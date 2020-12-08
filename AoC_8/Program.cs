using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_8
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt").ToList();
            var accumulator = 0;
            for(int i = 0; i < lines.Count(); i++)
            {
                var copylines = new List<string>(lines);
                var instruction = 0;
                var nopcommand = lines[i];
                var nparts = nopcommand.Split(' ');
                var nop = nparts[0];

                if (String.Equals(nop, "jmp") || String.Equals(nop, "nop"))
                {
                    if (String.Equals(nop, "jmp")) copylines[i] = copylines[i].Replace("jmp", "nop");
                    else copylines[i] = copylines[i].Replace("nop", "jmp");
                }
                else
                {
                    continue;
                }

                accumulator = 0;
                bool looping = false;
                List<int> executed = new List<int>();
                do
                {
                    executed.Add(instruction);
                    var opcommand = copylines[instruction];
                    var parts = opcommand.Split(' ');
                    var op = parts[0];
                    var value = int.Parse(parts[1].Replace("+", ""));
                    switch (op)
                    {
                        case "acc":
                            accumulator = accumulator + value;
                            instruction++;
                            break;
                        case "jmp":
                            instruction = instruction + value;
                            break;
                        case "nop":
                            instruction++;
                            break;
                    }
                    if (executed.Contains(instruction)) 
                    {
                        looping = true;
                    }
                } while (!looping && instruction < lines.Count());

                if (!looping)
                {
                    break;
                }
            }
            
            Console.WriteLine(accumulator);
        }

        private void Part1()
        {
            var lines = File.ReadAllLines("input.txt");
            var accumulator = 0;
            var instruction = 0;
            List<int> executed = new List<int>();
            do
            {
                executed.Add(instruction);
                var opcommand = lines[instruction];
                var parts = opcommand.Split(' ');
                var op = parts[0];
                var value = int.Parse(parts[1].Replace("+", ""));

                switch (op)
                {
                    case "acc":
                        accumulator = accumulator + value;
                        instruction++;
                        break;
                    case "jmp":
                        instruction = instruction + value;
                        break;
                    case "nop":
                        instruction++;
                        break;
                }
            } while (!executed.Contains(instruction));
            Console.WriteLine(accumulator);
        }
    }
}
