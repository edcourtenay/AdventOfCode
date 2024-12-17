namespace AdventOfCode.Solutions.Year2023;

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
            .Sum(points => Point<long>.ManhattanDistance(points[0], points[1]));
    }

    public static IEnumerable<Point<long>> Parse(string input)
    {
        return input.ToLines()
            .Select<string, (string line, int y)>((line, y) => (line, y))
            .SelectMany(t => t.line.Select((c, x) => (c, x, t.y)).Where(u => u.c == '#'))
            .Select(t => new Point<long>(t.x, t.y));
    }

    public static IEnumerable<Point<long>> Expand(IEnumerable<Point<long>> source, long expansionFactor)
    {
        List<Point<long>> points = source.ToList();
        return ExpandAxis(
            ExpandAxis(points, points.MinMaxBy(p => p.X), (point, i) => point.X == i,
                (point, i) => (point.X + i, point.Y), expansionFactor), points.MinMaxBy(p => p.Y),
            (point, i) => point.Y == i, (point, i) => (point.X, point.Y + i), expansionFactor);
    }

    private static IEnumerable<Point<long>> ExpandAxis(IEnumerable<Point<long>> source, (long min, long max) extents, Func<Point<long>, long, bool> predicate, Func<Point<long>, long, Point<long>> generator, long expansionFactor)
    {
        var points = source.ToList();

        var expansion = 0L;
        for (long i = extents.min; i <= extents.max; i++)
        {
            var j = i;
            var set = points.Where(p => predicate(p, j)).ToArray();

            if (set.Length == 0)
            {
                expansion += (expansionFactor - 1);
                continue;
            }

            foreach (Point<long> point in set)
            {
                yield return generator(point, expansion);
            }
        }
    }
}