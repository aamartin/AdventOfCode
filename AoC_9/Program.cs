using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_9
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var preambleLen = 25;

            int value = 0;
            int i = 0;
            for (i = preambleLen; i < lines.Length; i++)
            {
                value = int.Parse(lines[i]);

                if (!CheckPreamble(lines, preambleLen, value, i))
                {
                    Console.WriteLine(value);
                    break;
                }
            }

            int sumrange = FindRange(lines, value, i);
            Console.WriteLine(sumrange);

        }

        private static int FindRange(string[] lines, int value, int indx)
        {
            int runningtotal = 0;

            for (int i = 0; i < indx; i++)
            {
                int small = int.MaxValue;
                int large = int.MinValue;

                var first = int.Parse(lines[i]);
                if (first > large) large = first;
                if (first < small) small = first;
                runningtotal = int.Parse(lines[i]);
                for(int j = i+1; j < indx; j++)
                {
                    var addvalue = int.Parse(lines[j]);
                    if (addvalue > large) large = addvalue;
                    if (addvalue < small) small = addvalue;
                    runningtotal = runningtotal + addvalue;
                    if (runningtotal == value)
                    {
                        return small + large;
                    }
                }
            }
            return -1;
        }

        private static bool CheckPreamble(string[] lines, int preambleLen, int value, int index)
        {
            for (int i = index-preambleLen; i < index + preambleLen - 1; i++)
            {
                var firstnum = int.Parse(lines[i]);
                for (int j = index-1; j > i; j--)
                {
                    var secondnum = int.Parse(lines[j]);
                    if (firstnum + secondnum == value)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
