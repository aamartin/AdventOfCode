using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace AoC_18
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            long part1 = 0;
            long part2 = 0;
            foreach(var line in lines)
            {
                var revLine = Reverse(line).Replace(" ", "");
                var answer = Solve(revLine);
                var p2answer = Solve(revLine, true);
                Console.WriteLine($"{line} = {answer} - {p2answer}");
                part1 = part1 + answer;
                part2 = part2 + p2answer;
            }
            Console.WriteLine($"Part 1: {part1}");
            Console.WriteLine($"Part 2: {part2}");
        }

        public static string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            for(int i = 0; i < charArray.Length; i++)
            {
                if (charArray[i] == ')') charArray[i] = '(';
                else if (charArray[i] == '(') charArray[i] = ')';
            }
            return new string(charArray);
        }

        private static long Solve(string line, bool checkPrecidence = false)
        {
            var values = new Stack<long>();
            var ops = new Stack<char>();
            var tokes = line.ToCharArray();

            for(int i = 0; i < tokes.Length; i++)
            {
                var token = tokes[i];
                switch(token)
                {
                    case '(':
                        ops.Push(token);
                        break;
                    case ')':
                        while(ops.Peek() != '(')
                        {
                            values.Push(eval(ops.Pop(), values.Pop(), values.Pop()));
                        }
                        ops.Pop();
                        break;
                    case '+':
                    case '*':
                        if (checkPrecidence)
                        {
                            while (ops.Count > 0 && HasPrecedence(token, ops.Peek()))
                            {
                                values.Push(eval(ops.Pop(), values.Pop(), values.Pop()));
                            }
                        }
                        ops.Push(token);
                        break;
                    default:
                        values.Push(long.Parse(token.ToString()));
                        break;

                }
            }

            while(ops.Count > 0)
            {
                values.Push(eval(ops.Pop(), values.Pop(), values.Pop()));
            }

            return values.Pop();
        }

        private static bool HasPrecedence(char op1, char op2)
        {
            if (op2 == '(' || op2 == ')')
            {
                return false;
            }
            if (op1 == '+' && op2 == '*') return false;
            else return true;
        }

        private static long eval(char op, long a, long b)
        {
            switch(op)
            {
                case '+':
                    return a + b;
                case '*':
                    return a * b;
            }
            return 0;
        }

    }
}
