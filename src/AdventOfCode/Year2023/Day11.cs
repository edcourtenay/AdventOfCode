using System.Numerics;

using Point = (long X, long Y);

namespace AdventOfCode.Year2023;

[Description("Cosmic Expansion")]
public class Day11 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, 2L);
    }

    public object Part2(string input)
    {
        return Solve(input, 1000000L);
    }

    public static long Solve(string input, long expansionFactor)
    {
        return Expand(Parse(input), expansionFactor).ToArray()
            .Combinations(2)
            .Select(tuples => tuples.ToArray())
            .Sum(tuples => GetManhattanDistance(tuples[0], tuples[1]));
    }

    public static IEnumerable<Point> Parse(string input)
    {
        return input.ToLines()
            .Select((line, y) => (line, y))
            .SelectMany(t => t.line.Select((c, x) => (c, x, t.y)).Where(t => t.c == '#'))
            .Select(t => ((long)t.x, (long)t.y));
    }

    public static IEnumerable<Point> Expand(IEnumerable<Point> source, long expansionFactor)
    {
        List<Point> points = source.ToList();
        (long minX, long maxX) = points.Select(p => p.X).MinMax();
        (long minY, long maxY) = points.Select(p => p.Y).MinMax();

        return ExpandAxis(
            ExpandAxis(points, minX, maxX, (point, i) => point.X == i, (point, i) => (point.X + i, point.Y), expansionFactor), minY,
            maxY, (point, i) => point.Y == i, (point, i) => (point.X, point.Y + i), expansionFactor);
    }

    private static IEnumerable<Point> ExpandAxis(IEnumerable<Point> source, long min, long max, Func<Point, long, bool> predicate, Func<Point, long, Point> generator, long expansionFactor)
    {
        var points = source.ToList();

        var expansion = 0L;
        for (long i = min; i <= max; i++)
        {
            var j = i;
            var set = points.Where(p => predicate(p, j)).ToArray();

            if (set.Length == 0)
            {
                expansion += (expansionFactor - 1);
                continue;
            }

            foreach (Point point in set)
            {
                yield return generator(point, expansion);
            }
        }
    }

    private static T GetManhattanDistance<T>((T x, T y) p1, (T x, T y) p2) where T : INumber<T>
    {
        return T.Abs(p1.x - p2.x) + T.Abs(p1.y - p2.y);
    }
}