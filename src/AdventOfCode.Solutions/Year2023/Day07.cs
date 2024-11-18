using System.Collections;

namespace AdventOfCode.Solutions.Year2023;

[Description("Camel Cards")]
public class Day07 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, false);
    }

    public object Part2(string input)
    {
        return Solve(input, true);
    }

    private static object Solve(string input, bool joker)
    {
        var comparer = new HandComparer(joker);

        return Enumerable
            .Select<string, Hand>(input.ToLines(), line => ParseLine(line, joker)).OrderBy(x => x, comparer)
            .Select((hand, i) => (Hand: hand, Rank: i + 1))
            .Sum(tuple => tuple.Hand.Bid * tuple.Rank);
    }

    private static Hand ParseLine(string line, bool joker)
    {
        if (line.Split(" ") is [{ } p0, { } p1])
            return new Hand(p0, int.Parse(p1), joker);

        throw new ArgumentException("Cannot parse line", nameof(line));
    }

    public record Hand
    {
        public Hand(string Cards, int Bid, bool joker = false)
        {
            this.Cards = Cards;
            this.Bid = Bid;

            var arr = Cards.Where(c => !joker || c != 'J').GroupBy(c => c).Select(g => g.Count()).OrderDescending().ToArray();
            var jokers = joker ? Cards.Count(c => c == 'J') : 0;

            HandType = (arr.FirstOrDefault() + jokers, arr) switch
            {
                (5, _) => HandTypes.FiveOfAKind,
                (4, _) => HandTypes.FourOfAKind,
                (3, [_, 2, ..]) => HandTypes.FullHouse,
                (3, _) => HandTypes.ThreeOfAKind,
                (2, [_, 2, ..]) => HandTypes.TwoPair,
                (2, _) => HandTypes.OnePair,
                _ => HandTypes.HighCard
            };
        }

        public string Cards { get; }
        public int Bid { get; }
        public HandTypes HandType { get; }
    }

    public enum HandTypes
    {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }

    private class HandComparer : IComparer<Hand?>
    {
        private readonly bool _joker;
        private const string CardOrder = "23456789TJQKA";
        private const string CardOrderWithJoker = "J23456789TQKA";

        public HandComparer(bool joker = false)
        {
            _joker = joker;
        }

        public int Compare(Hand? x, Hand? y)
        {
            if (x == null || y == null)
            {
                return Comparer.Default.Compare(x, y);
            }

            var handTypeComparison = x.HandType.CompareTo(y.HandType);
            if (handTypeComparison != 0)
            {
                return handTypeComparison;
            }

            var cardOrder = _joker ? CardOrderWithJoker : CardOrder;

            return x.Cards.Select(c => cardOrder.IndexOf(c)).Zip(y.Cards.Select(c => cardOrder.IndexOf(c)))
                .Select(tuple => tuple.First.CompareTo(tuple.Second)).FirstOrDefault(c => c != 0);
        }
    }
}