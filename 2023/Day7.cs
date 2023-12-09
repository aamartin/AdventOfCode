using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AoC2023
{
    internal class Day7
    {
        internal static void Execute(string v)
        {
            Console.WriteLine("Day 6");
            Console.WriteLine($"Part1: {Part1(v)}");
            Console.WriteLine($"Part2: {Part2(v)}");
            Console.WriteLine();
        }

        class Hand : IComparable<Hand>
        {
            public Hand(string hand, int bid, bool part2 = false)
            {
                this.hand = hand;
                this.Bid = bid;
                this.part2 = part2;
                ParseHand();
            }

            private Dictionary<char, int> handHash = new Dictionary<char, int>();

            private void ParseHand()
            {
                foreach(var card in hand.ToCharArray())
                {
                    if(handHash.ContainsKey(card)) handHash[card]++;
                    else handHash[card] = 1;
                }
            }

            private HandType calcHandType()
            {
                if (handHash.Values.Any(x => x == 5))
                    return HandType.FiveOfAKind;
                else if (handHash.Values.Any(x => x == 4))
                    return HandType.FourOfAKind;
                else if (handHash.Values.Any(x => x == 3) && handHash.Values.Any(x => x == 2))
                    return HandType.FullHouse;
                else if (handHash.Values.Any(x => x == 3))
                    return HandType.ThreeOfAKind;
                else if (handHash.Values.Count(x => x == 2) == 2)
                    return HandType.TwoPairs;
                else if (handHash.Values.Any(x => x == 2))
                    return HandType.OnePair;
                else
                    return HandType.HighCard;
            }

            private HandType calcHandTypeP2()
            {
                var handType = calcHandType();

                if (!hand.Contains("J"))
                    return handType;

                var count = handHash['J'];
                switch (handType)
                {
                    case HandType.FiveOfAKind:
                        return HandType.FiveOfAKind;
                    case HandType.FourOfAKind:
                        return HandType.FiveOfAKind;
                    case HandType.FullHouse:
                        if (count == 1)
                            return HandType.FourOfAKind;
                        else
                            return HandType.FiveOfAKind;
                    case HandType.ThreeOfAKind:
                        return HandType.FourOfAKind;
                    case HandType.TwoPairs:
                        if (count == 1)
                            return HandType.FullHouse;
                        else
                            return HandType.FourOfAKind;
                    case HandType.OnePair: return HandType.ThreeOfAKind;
                    case HandType.HighCard: return HandType.OnePair;
                    default: return handType;
                }
            }

            public int CompareTo(Hand other)
            {
                if( (int) this.Type == (int) other.Type)
                {
                    var thisCards = this.hand.ToCharArray();
                    var otherCards = other.hand.ToCharArray();
                    for(int i = 0; i < thisCards.Length; i++)
                    {
                        if(thisCards[i] != otherCards[i]) { 
                            return getCardValue(thisCards[i]).CompareTo(getCardValue(otherCards[i])); }
                    }

                    return 0; 
                }
                else
                {
                    return this.Type.CompareTo(other.Type);
                }
            }

            private int getCardValue(char v)
            {
                switch (v)
                {
                    case 'A':
                        return 14;
                    case 'K':
                        return 13;
                    case 'Q':
                        return 12;
                    case 'J':
                        if (part2)
                            return 1;
                        else 
                            return 11;
                    case 'T':
                        return 10;
                    default:
                        return int.Parse(v.ToString());
                }
            }

            public int Type
            {
                get
                {
                    if (!part2)
                        return (int)calcHandType();
                    else
                        return (int)calcHandTypeP2();
                }
            }

            private string hand;

            public int Bid { get; internal set; }

            private bool part2;
        }

        public enum HandType
        {
            HighCard = 0,
            OnePair = 1,
            TwoPairs = 2,
            ThreeOfAKind = 3,
            FullHouse = 4,
            FourOfAKind = 5,
            FiveOfAKind = 6
        }

        private static int Part1(string v)
        {
            var result = 0;
            var cards = ParseFile(v);
            cards.Sort();
            for(int i = 0; i < cards.Count; i++)
            {
                var total = ((i + 1) * cards[i].Bid);
                result = result + total;
            }
            
            return result;
        }

        private static List<Hand> ParseFile(string v, bool part2 = false)
        {
            var hands = new List<Hand>();
            foreach(var line in File.ReadLines(v))
            {
                var parts = line.Split(" ");
                hands.Add(new Hand(parts[0], int.Parse(parts[1]), part2));
            }
            return hands;
        }

        private static object Part2(string v)
        {
            var result = 0;
            var cards = ParseFile(v, true);
            cards.Sort();
            for (int i = 0; i < cards.Count; i++)
            {
                var total = ((i + 1) * cards[i].Bid);
                result = result + total;
            }

            return result;
        }
    }
}
