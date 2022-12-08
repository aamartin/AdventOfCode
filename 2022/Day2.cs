using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AoC2022
{
    class Day2
    {
        internal static void Execute(string file)
        {
            Console.WriteLine("Day 2");
            Console.WriteLine($"Part1: {Part1(file)}");
            Console.WriteLine($"Part2: {Part2(file)}");
            Console.WriteLine();
        }

        private static int Part2(string file)
        {
            var totalScore = 0;
            foreach (var line in File.ReadAllLines(file))
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var elf = Convert(parts[0]);
                var me = parts[1];
                var myHand = ChooseType(elf, me);
                var matrixTotal = CalcScore(elf, myHand);
                totalScore += matrixTotal;
            }
            return totalScore;
        }

        private static String ChooseType(string elf, string me)
        {
            switch(me)
            {
                case "Y":
                    return elf;
                case "X": // Lose
                    switch (elf)
                    {
                        case "Rock": return "Scissors";
                        case "Paper": return "Rock";
                        case "Scissors": return "Paper";
                    }
                    throw new Exception();
                case "Z": // win
                    switch (elf)
                    {
                        case "Rock": return "Paper";
                        case "Paper": return "Scissors";
                        case "Scissors": return "Rock";
                    }
                    throw new Exception();
            }
            throw new Exception();
        }

        private static int Part1(string file)
        {
            var totalScore = 0;
            foreach(var line in File.ReadAllLines(file))
            {
                var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var elf = Convert(parts[0]);
                var me = Convert(parts[1]);
                var matrixTotal = CalcScore(elf, me);
                var logicaltotal = ComputeRound(line);
                if (logicaltotal != matrixTotal) throw new Exception("mismatch");
                totalScore += logicaltotal;
            }
            return totalScore;
        }

        private static string Convert(string v)
        {
            switch (v)
            {
                case "A":
                case "X":
                    return "Rock";
                case "B":
                case "Y":
                    return "Paper";
                case "C":
                case "Z":
                    return "Scissors";
                default:
                    throw new Exception($"Unknown type {v}");
            }
        }

        private static int CalcScore(string elf, string me)
        {
            if (String.Equals(elf, me)) return 3 + ConvertToInt(me);
            var join = $"{elf},{me}";
            switch(join)
            {
                case "Rock,Paper":
                case "Scissors,Rock":
                case "Paper,Scissors":
                    return 6 + ConvertToInt(me);
                case "Rock,Scissors":
                case "Paper,Rock":
                case "Scissors,Paper":
                    return 0 + ConvertToInt(me);
            }
            throw new Exception();
        }

        private static int ConvertToInt(string me)
        {
            switch(me)
            {
                case "Rock":
                    return 1;
                case "Paper":
                    return 2;
                case "Scissors":
                    return 3;
                default:
                    throw new Exception();

            }
        }

        private static int ComputeRound(string v)
        {
            switch(v)
            {
                case "A X":
                    return 1 + 3;
                case "A Y":
                    return 2 + 6;
                case "A Z":
                    return 3 + 0;
                case "B X":
                    return 1 + 0;
                case "B Y":
                    return 2 + 3;
                case "B Z":
                    return 3 + 6;
                case "C X":
                    return 1 + 6;
                case "C Y":
                    return 2 + 0;
                case "C Z":
                    return 3 + 3;
                default:
                    throw new Exception("what!?");
            }
        }
    }
}
