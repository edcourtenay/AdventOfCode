using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022;

[Description("Regolith Reservoir")]
public partial class Day14 : IPuzzle
{

    public object Part1(string input)
    {
        var set = input.ToLines()
            .SelectMany(ParseLine)
            .ToHashSet();

        var bottomRow = set.Max(position => position.Y);

        var count = 0;
        while (Drop((500, 0), set, bottomRow, false))
        {
            count++;
        }

        return count;
    }

    public object Part2(string input)
    {
        var set = input.ToLines()
            .SelectMany(ParseLine)
            .ToHashSet();

        var bottomRow = set.Max(position => position.Y);

        var count = 0;
        (int X, int Y) dropFrom = new(500, 0);

        while (!set.Contains(dropFrom))
        {
            Drop(dropFrom, set, bottomRow, true);
            count++;
        }

        return count;
    }

    private static IEnumerable<(int X, int Y)> ParseLine(string line)
    {
        var list = PointsRegex().Matches(line)
            .Where(match => match.Success)
            .Select(match => (int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["y"].Value)));

        foreach (((int X, int Y) first, (int X, int Y) second) in list.Pairwise())
        {
            var current = first;
            int dx = second.X.CompareTo(first.X);
            int dy = second.Y.CompareTo(first.Y);

            do
            {
                yield return current;
            } while (((current = (current.X + dx, current.Y + dy)) != second));

            yield return second;
        }
    }

    public bool Drop((int X, int Y) start, HashSet<(int X, int Y)> positions, int bottom, bool useFloor)
    {
        bool CheckEmpty((int X, int Y) p) => (!useFloor || p.Y < bottom + 2) && !positions.Contains(p);

        var current = start;
        bool belowBottom = false;

        while (true)
        {
            (int X, int Y) down = (current.X, current.Y + 1);
            (int X, int Y) downLeft = (current.X - 1, current.Y + 1);
            (int X, int Y) downRight = (current.X + 1, current.Y + 1);

            var moveTo = current;
            if (CheckEmpty(down))
            {
                moveTo = down;
            }
            else if (CheckEmpty(downLeft))
            {
                moveTo = downLeft;
            }
            else if (CheckEmpty(downRight))
            {
                moveTo = downRight;
            }

            if (moveTo == current || (belowBottom = (moveTo.Y >= bottom + 2)))
            {
                break;
            }

            current = moveTo;
        }

        positions.Add(current);

        return !belowBottom;
    }

    [GeneratedRegex("(?<x>\\d+),(?<y>\\d+)", RegexOptions.Compiled)]
    private static partial Regex PointsRegex();
}