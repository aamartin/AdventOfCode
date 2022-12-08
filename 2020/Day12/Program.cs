using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC_12
{
    class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");

            var ship = new Ship();
            var ship2 = new ShipP2();
            foreach (var line in lines)
            {
                var op = line.Take(1).ToArray()[0];
                var amount = int.Parse(line.Remove(0, 1));
                switch (op)
                {
                    case 'N':
                        ship.North(amount);
                        ship2.North(amount);
                        break;
                    case 'S':
                        ship.South(amount);
                        ship2.South(amount);
                        break;
                    case 'E':
                        ship.East(amount);
                        ship2.East(amount);
                        break;
                    case 'W':
                        ship.West(amount);
                        ship2.West(amount);
                        break;
                    case 'L':
                        ship.Left(amount);
                        ship2.Left(amount);
                        break;
                    case 'R':
                        ship.Right(amount);
                        ship2.Right(amount);
                        break;
                    case 'F':
                        ship.Forward(amount);
                        ship2.Forward(amount);
                        break;

                }
            }

            int mDist = ship.ManhattanDistance();
            Console.WriteLine(mDist);

            double mDist2 = ship2.ManhattanDistance();
            Console.WriteLine(mDist2);
        }
    }

    internal class ShipP2
    {
        private double x;
        private double y;
        private int wpx;
        private int wpy;

        public ShipP2()
        {
            x = 0;
            y = 0;
            wpy = 1;
            wpx = 10;
        }

        internal double ManhattanDistance()
        {
            return Math.Abs(x) + Math.Abs(y);
        }

        internal void East(int amount)
        {
            wpx = wpx + amount;
        }

        internal void Forward(int amount)
        {
            x += wpx * amount;
            y += wpy * amount;
        }

        internal void Left(int amount)
        {
            if (amount % 90 != 0) throw new Exception();
            for (int i = amount; i > 0; i = i - 90)
            {
                var tmp = wpx;
                wpx = -wpy;
                wpy = tmp;
            }
        }

        private int GetQuad(int xpos, int ypos)
        {
            if (xpos >= 0 && ypos >= 0) return 0;
            else if (xpos >= 0 && ypos < 0) return 1;
            else if (xpos < 0 && ypos < 0) return 2;
            else return 3;
        }

        internal void North(int amount)
        {
            wpy = wpy + amount;
        }

        internal void Right(int amount)
        {
            if (amount % 90 != 0) throw new Exception();
            for (int i = amount; i > 0; i = i - 90)
            {
                var tmp = wpx;
                wpx = wpy;
                wpy = -tmp;
            }
        }

        internal void South(int amount)
        {
            wpy = wpy - amount;
        }

        internal void West(int amount)
        {
            wpx = wpx - amount;
        }

    }

    internal class Ship
    {
        private int x;
        private int y;
        private int xDirection;
        private int yDirection;
        

        public Ship()
        {
            x = 0;
            y = 0;
            xDirection = 1; // East
            yDirection = 0; 
        }

        internal int ManhattanDistance()
        {
            return Math.Abs(x) + Math.Abs(y);
        }

        internal void East(int amount)
        {
            x = x + amount;
        }

        internal void Forward(int amount)
        {
            x = x + (xDirection * amount);
            y = y + (yDirection * amount);
        }

        internal void Left(int amount)
        {
            if (amount % 90 != 0) throw new Exception();
            for (int i = amount; i > 0; i = i -90)
            {
                if (xDirection == 1) { xDirection = 0; yDirection = 1; }
                else if (xDirection == -1) { xDirection = 0; yDirection = -1; }
                else if (yDirection == -1) { xDirection = 1; yDirection = 0; }
                else if (yDirection == 1) { xDirection = -1; yDirection = 0; }
            }
        }

        internal void North(int amount)
        {
            y = y + amount;
        }

        internal void Right(int amount)
        {
            if (amount % 90 != 0) throw new Exception();
            for (int i = amount; i > 0; i = i - 90)
            {
                if (xDirection == 1) { xDirection = 0; yDirection = -1; }
                else if (xDirection == -1) { xDirection = 0; yDirection = 1; }
                else if (yDirection == -1) { xDirection = -1; yDirection = 0; }
                else if (yDirection == 1) { xDirection = 1; yDirection = 0; }
            }
        }

        internal void South(int amount)
        {
            y = y - amount;
        }

        internal void West(int amount)
        {
            x = x - amount;
        }
    }
}
