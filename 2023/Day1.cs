using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AoC2022
{
    class Day1
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 1");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part1(string file)
        {
            int total = 0;
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                char first = 'z', last = 'z';
                var elements = line.ToCharArray();
                int start = 0;
                int end = elements.Length;
                do
                {
                    if (first == 'z' && Char.IsDigit(elements[start])) first = elements[start];
                    if (last == 'z' && Char.IsDigit(elements[end - 1])) last = elements[end - 1];
                    start++;
                    end--;
                }
                while (last == 'z' || first == 'z');
                var value = $"{first}{last}";
                total = total + (int.Parse(value));
            }

            return total;
        }

        private static int Part2(string file)
        {
            int total = 0;
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                char first = 'z', last = 'z';
                var elements = line.ToCharArray();
                int start = 0;
                int end = elements.Length;
                do
                {
                    var checkFirst = CheckDigit(line, start, true);
                    if (first == 'z' && checkFirst != 'z') first = checkFirst;
                    var checkLast = CheckDigit(line, end-1, false);
                    if (last == 'z' && checkLast != 'z') last = checkLast;
                    start++;
                    end--;
                }
                while (last == 'z' || first == 'z');
                var value = $"{first}{last}";
                total = total + (int.Parse(value));
            }

            return total;
        }

        private static char CheckDigit(string line, int index, bool v)
        {
            var startIdx = index;
            if (Char.IsDigit(line.ToCharArray()[index])) return line.ToCharArray()[index];
            if (!v)
            {
                line = ReverseStr(line);
                startIdx = line.Length - index-1;
            }
            char digit = ParseDigit(line.Substring(startIdx, line.Length-startIdx));
            if (Char.IsDigit(digit)) return digit; 

            return 'z';
        }

        private static char ParseDigit(string line)
        {
            if (line.StartsWith("one") || line.StartsWith(ReverseStr("one"))) return '1';
            if (line.StartsWith("two") || line.StartsWith(ReverseStr("two"))) return '2';
            if (line.StartsWith("three") || line.StartsWith(ReverseStr("three"))) return '3';
            if (line.StartsWith("four") || line.StartsWith(ReverseStr("four"))) return '4';
            if (line.StartsWith("five") || line.StartsWith(ReverseStr("five"))) return '5';
            if (line.StartsWith("six") || line.StartsWith(ReverseStr("six"))) return '6';
            if (line.StartsWith("seven") || line.StartsWith(ReverseStr("seven"))) return '7';
            if (line.StartsWith("eight") || line.StartsWith(ReverseStr("eight"))) return '8';
            if (line.StartsWith("nine") || line.StartsWith(ReverseStr("nine"))) return '9';
            if (line.StartsWith("zero") || line.StartsWith(ReverseStr("zero"))) return '0';
            return 'z';
        }

        public static string ReverseStr(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }
    }
}
