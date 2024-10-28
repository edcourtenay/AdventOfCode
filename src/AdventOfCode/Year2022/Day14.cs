using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022;

[Description("Regolith Reservoir")]
public partial class Day14 : IPuzzle
{
    public object Part1(string input) => Solve(input, (500, 0), false);

    public object Part2(string input) => Solve(input, (500, 0), true);

    private static object Solve(string input, (int, int) start, bool useFloor)
    {
        var set = input.ToLines()
            .SelectMany(ParseLine)
            .ToHashSet();

        return Drop(start, set, useFloor)
            .Count();
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

    private static IEnumerable<(int X, int Y)> Drop((int X, int Y) start, HashSet<(int X, int Y)> set, bool useFloor)
    {
        var stack = new Stack<(int X, int Y)>([start]);

        foreach ((int X, int Y) position in Drop(stack, set, set.Max(p => p.Y), useFloor))
        {
            set.Add(position);
            yield return position;
        }
    }

    private static IEnumerable<(int X, int Y)> Drop(Stack<(int X, int Y)> stack, IReadOnlySet<(int X, int Y)> set, int bottom, bool useFloor)
    {
        bool CheckEmpty((int X, int Y) p) => (!useFloor || p.Y < bottom) && !set.Contains(p);

        if (useFloor)
        {
            bottom += 2;
        }

        while (stack.TryPop(out var current))
        {
            while (true)
            {
                if (current.Y >= bottom)
                {
                    yield break;
                }

                (int X, int Y) down = (current.X, current.Y + 1);
                (int X, int Y) downLeft = (current.X - 1, current.Y + 1);
                (int X, int Y) downRight = (current.X + 1, current.Y + 1);

                var moveTo = (CheckEmpty(downLeft), CheckEmpty(down), CheckEmpty(downRight)) switch
                {
                    (_, true, _) => down,
                    (true, false, _) => downLeft,
                    (false, false, true) => downRight,
                    _ => current
                };

                if (moveTo == current)
                {
                    break;
                }

                stack.Push(current);
                current = moveTo;
            }

            yield return current;
        }
    }

    [GeneratedRegex(@"(?<x>\d+),(?<y>\d+)", RegexOptions.Compiled)]
    private static partial Regex PointsRegex();
}