using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2022;

[Description("Regolith Reservoir")]
public partial class Day14 : IPuzzle
{
    public object Part1(string input) => Solve(input, (500, 0), false);

    public object Part2(string input) => Solve(input, (500, 0), true);

    private static object Solve(string input, Point start, bool useFloor)
    {
        var set = input.ToLines()
            .SelectMany(ParseLine)
            .ToHashSet();

        return Drop(start, set, useFloor)
            .Count();
    }

    private static IEnumerable<Point> ParseLine(string line)
    {
        var list = PointsRegex().Matches(line)
            .Where(match => match.Success)
            .Select(match => (int.Parse(match.Groups["x"].Value), int.Parse(match.Groups["y"].Value)));

        foreach ((Point first, Point second) in list.Pairwise())
        {
            var current = first;
            Direction d = new(second.X.CompareTo(first.X), second.Y.CompareTo(first.Y));

            do
            {
                yield return current;
            } while (((current += d) != second));

            yield return second;
        }
    }

    private static IEnumerable<Point> Drop(Point start, HashSet<Point> set, bool useFloor)
    {
        var stack = new Stack<Point>([start]);

        foreach (Point position in Drop(stack, set, set.Max(p => p.Y), useFloor))
        {
            set.Add(position);
            yield return position;
        }
    }

    private static IEnumerable<Point> Drop(Stack<Point> stack, IReadOnlySet<Point> set, int bottom, bool useFloor)
    {
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

                Point down = current + Direction.South;
                Point downLeft = current + Direction.SouthWest;
                Point downRight = current + Direction.SouthEast;

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

        yield break;

        bool CheckEmpty(Point p) => (!useFloor || p.Y < bottom) && !set.Contains(p);
    }

    [GeneratedRegex(@"(?<x>\d+),(?<y>\d+)", RegexOptions.Compiled)]
    private static partial Regex PointsRegex();
}