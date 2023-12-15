using System.Text.RegularExpressions;

namespace AdventOfCode.Year2018;

[Description("No Matter How You Slice It")]
public partial class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var claims = input.ToLines(Claim.Parse);

        Dictionary<(int x, int y), List<int>> dict = ParseClaims(claims);

        return dict.Values.Count(l => l.Count > 1);
    }

    public object Part2(string input)
    {
        var claims = input.ToLines(Claim.Parse).ToArray();

        Dictionary<(int x, int y), List<int>> dict = ParseClaims(claims);

        return (claims.Where(claim => claim.Positions().All(pos => dict[pos].Count == 1))
            .Select(claim => claim.Id)).FirstOrDefault();
    }

    private static Dictionary<(int x, int y), List<int>> ParseClaims(IEnumerable<Claim> claims)
    {
        var dict = new Dictionary<(int x, int y), List<int>>();

        foreach (Claim claim in claims)
        {
            foreach ((int x, int y) pos in claim.Positions())
            {
                if (dict.TryGetValue(pos, out List<int>? list))
                {
                    list.Add(claim.Id);
                }
                else
                {
                    dict[pos] = [..new[] { claim.Id }];
                }
            }
        }

        return dict;
    }

    public record Claim
    {
        public required int Id { get; init; }
        public required (int X, int Y) Position { get; init; }
        public required int Width { get; init; }
        public required int Height { get; init; }

        public IEnumerable<(int x, int y)> Positions()
        {
            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    yield return (Position.X + x, Position.Y + y);
                }
            }
        }

        public static Claim Parse(string input)
        {
            Match match = ClaimRegex().Match(input);

            return new Claim
            {
                Id = int.Parse(match.Groups["id"].Value),
                Position = (int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["y"].Value)),
                Width = int.Parse(match.Groups["width"].Value),
                Height = int.Parse(match.Groups["height"].Value)
            };
        }
    }

    [GeneratedRegex("#(?<id>\\d+) @ (?<x>\\d+),(?<y>\\d+): (?<width>\\d+)x(?<height>\\d+)", RegexOptions.Compiled)]
    private static partial Regex ClaimRegex();
}