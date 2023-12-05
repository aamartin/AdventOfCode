using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023
{
    class Card
    {
        public Card()
        {
            WinningNumbers = new HashSet<int>();
            Numbers = new HashSet<int>();
        }
        public string ID { get; set; }
        public HashSet<int> WinningNumbers { get; set; }
        public HashSet<int> Numbers { get; set; }
    }

    internal class Day4
    {

        internal static void Execute(string v)
        {
            Console.WriteLine("Day 4");
            Console.WriteLine($"Part1: {Part1(v)}");
            Console.WriteLine($"Part2: {Part2(v)}");
            Console.WriteLine();
        }

        private static int Part1(string file)
        {
            List<Card> cards = ParseCards(file);
            var totalPoints = 0;
            foreach (Card card in cards)
            {
                var pickedNums = new List<int>();
                var points = 1;
                bool first = true;
                foreach(var winningNum in card.WinningNumbers)
                {
                    
                    if(card.Numbers.Contains(winningNum)) 
                        pickedNums.Add(winningNum);
                }

                foreach(var winner in pickedNums)
                {
                    if (first) { first = false; }
                    else
                    {
                        points = points * 2;
                    }
                }

                if(pickedNums.Any())
                    totalPoints = totalPoints + points;
            }

            return totalPoints;
        }

        private static List<Card> ParseCards(string file)
        {
            List<Card> cards = new List<Card>();
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var lineParts = line.Split(':');
                var card = new Card();
                card.ID = lineParts[0].Replace("Card ", "");
                var numParts = lineParts[1].Split('|');

                foreach (var item in numParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList())
                    card.WinningNumbers.Add(int.Parse(item));
                foreach (var item in numParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList())
                    card.Numbers.Add(int.Parse(item));

                cards.Add(card);
            }
            return cards;
        }

        private static Dictionary<Card, int> ParseCardsPart2(string file)
        {
            Dictionary<Card, int> cards = new Dictionary<Card, int>();
            var lines = File.ReadAllLines(file);
            foreach (var line in lines)
            {
                var lineParts = line.Split(':');
                var card = new Card();
                card.ID = lineParts[0].Replace("Card ", "");
                var numParts = lineParts[1].Split('|');

                foreach (var item in numParts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList())
                    card.WinningNumbers.Add(int.Parse(item));
                foreach (var item in numParts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList())
                    card.Numbers.Add(int.Parse(item));

                cards.Add(card, 1);
            }
            return cards;
        }

        private static int Part2(string file)
        {
            Dictionary<int, int> winningCards = new Dictionary<int, int>();
            var cards = ParseCards(file);
            foreach(var card in cards)
            {
                var cardId = int.Parse(card.ID);
                AddCopy(winningCards, cardId);

                for (var times = 0; times < winningCards[cardId]; times++)
                {
                    var pickedNums = new List<int>();
                    foreach (var winningNum in card.WinningNumbers)
                    {
                        if (card.Numbers.Contains(winningNum))
                            pickedNums.Add(winningNum);
                    }


                    for (int i = 0; i < pickedNums.Count; i++)
                    {
                        var copyCardNum = cardId + i + 1;
                        AddCopy(winningCards, copyCardNum);
                    }
                }
            }

            var total = 0;
            foreach(var cnt in winningCards.Values)
            {
                total = total + cnt;
            }
            return total;
        }

        private static void AddCopy(Dictionary<int, int> winningCards, int cardId)
        {
            int cardNums;
            if (!winningCards.TryGetValue(cardId, out cardNums))
            {
                winningCards[cardId] = 0;
            }
            winningCards[cardId]++;
        }
    }
}
