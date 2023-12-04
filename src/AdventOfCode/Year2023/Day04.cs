using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023;

[Description("Scratchcards")]
public partial class Day04 : IPuzzle
{
    [GeneratedRegex(@"Card\s+(?'card'\d+):(?'foo'\s+\d+)*\s\|(?'bar'\s+\d+)*", RegexOptions.Compiled)]
    public static partial Regex MyRegex();

    public object Part1(string input)
    {
        return input.ToLines()
            .Select(ParseLine)
            .Sum(card => card.Points);
    }

    public object Part2(string input)
    {
        var cards = input.ToLines()
            .Select(ParseLine)
            .ToArray();

        var piles = Enumerable.Range(0, cards.Length).Select(_ => 1).ToArray();

        foreach (Card card in cards)
        {
            for (int j = 1; j <= card.MatchCount; ++j)
            {
                piles[card.CardId + j -1] += piles[card.CardId - 1];
            }
        }

        return piles.Sum();
    }


    private (int, int) Day4(string p)
    {
        int[] t = File.ReadAllLines(p).Select(s => s.Split
                    (':')[1].Split('|').Select(s => s.Split(' ', (StringSplitOptions)1)).ToArray())
                .Select(v => v[0].Intersect(v[1]).Count()).ToArray(),
        u = new int[t.Length];
        Array.Fill(u, 1);
        for (int i = 0; i < t.Length; ++i)
        {
            for (int j = 1; j <= t[i]; ++j)
            {
                u[i + j] += u[i];
            }
        }

        return (t.Sum(c => (1 << c) >> 1), u.Sum());
    }

    public Card ParseLine(string line)
    {
        Match match = MyRegex().Match(line);
        int cardId = int.Parse(match.Groups["card"].Value);
        int[] winningNumbers = match.Groups["foo"].Captures.Select(c => int.Parse(c.Value)).ToArray();
        int[] numbers = match.Groups["bar"].Captures.Select(c => int.Parse(c.Value)).ToArray();

        return new Card(cardId, winningNumbers, numbers);
    }

    public record Card(int CardId, int[] WinningNumbers, int[] Numbers)
    {
        public int Points
        {
            get
            {
                int count = WinningNumbers.Intersect(Numbers).Count();
                return (count > 0 ? 1 : 0) << (count - 1);
            }
        }

        public int MatchCount => WinningNumbers.Intersect(Numbers).Count();
    };
}