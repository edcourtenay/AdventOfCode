using System.Text.RegularExpressions;

namespace AdventOfCode.Year2019;

[Description("Crossed Wires")]
public partial class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var lists = input.ToLines()
            .Select(ParseLine)
            .ToArray();

        return lists[0].Intersect(lists[1])
            .Min(tuple => Math.Abs(tuple.X) + Math.Abs(tuple.Y));
    }

    public object Part2(string input)
    {
        var lists = input.ToLines()
            .Select(ParseLine)
            .ToArray();

        return lists[0].Intersect(lists[1])
            .Min(tuple => lists.Sum(list => list.IndexOf(tuple) + 1));
    }

    private List<(int X, int Y)> ParseLine(string arg)
    {
        var list = new List<(int X, int Y)>();
        var position = (X: 0, Y: 0);

        foreach (Match match in MyRegex().Matches(arg))
        {
            (int X, int Y) direction = match.Groups["direction"].Value switch
            {
                "U" => (0, 1),
                "D" => (0, -1),
                "L" => (-1, 0),
                "R" => (1, 0),
                _ => throw new ArgumentOutOfRangeException()
            };

            int d = int.Parse(match.Groups["distance"].Value);
            for (int i = 0; i < d; i++)
            {
                position = (position.X + direction.X, position.Y + direction.Y);
                list.Add(position);
            }
        }

        return list;
    }

    [GeneratedRegex("(?<direction>[UDLR])(?<distance>\\d+)", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}