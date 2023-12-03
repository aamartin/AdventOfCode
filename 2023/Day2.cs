using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023
{
    class Game
    {
        public Game()
        {
            Sets = new List<Dictionary<string, int>>();
            ID = 0;
        }
        public int ID { get; set; }
        public List<Dictionary<string, int>> Sets { get; set; }

        public int GetPower()
        {
            int blue = 0, red = 0, green = 0;
            foreach(var set in Sets)
            {
                if (set.ContainsKey("red") && set["red"] > red) red = set["red"];
                if (set.ContainsKey("green") && set["green"] > green) green = set["green"];
                if (set.ContainsKey("blue") && set["blue"] > blue) blue = set["blue"];
            }
            return blue * red * green;
        }

        internal static Game ParseGame(string line)
        {
            // Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green
            var gamePart = line.Split(':');
            var game = new Game();
            game.ID = int.Parse(gamePart[0].Split(' ')[1]);
            foreach(var gameSetPart in gamePart[1].Split(';'))
            {
                var currentSet = new Dictionary<string, int>();
                // " 3 blue, 4 red"," 1 red, 2 green, 6 blue"," 2 green"
                foreach(var cubePart in gameSetPart.Split(','))
                {
                    // " 3 blue"," 4 red"
                    var cubeParts = cubePart.TrimStart().Split(' ');
                    currentSet[cubeParts[1]] = int.Parse(cubeParts[0]);
                }
                game.Sets.Add(currentSet);
            }
            return game;
        }
    }

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
            int total = 0;
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var game = Game.ParseGame(line);
                var power = game.GetPower();
                total = total + power;
            }
            return total;
        }

        private static int Part1(string file)
        {
            int total = 0;
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                bool passes = true;
                var game = Game.ParseGame(line);
                foreach(var set in game.Sets)
                {

                    if ((set.ContainsKey("red") && set["red"] > 12) || 
                        (set.ContainsKey("green") && set["green"] > 13) ||
                        (set.ContainsKey("blue") && set["blue"] > 14))
                    {
                        passes = false;
                        break;
                    }
                }

                if (passes) total = total + game.ID;

            }
            return total;
        }
    }
}
