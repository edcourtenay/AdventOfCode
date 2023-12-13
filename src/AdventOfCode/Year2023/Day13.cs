using Point = (int X, int Y);
using Pattern = System.Collections.Generic.IReadOnlySet<(int X, int Y)>;
using Patterns = System.Collections.Generic.IEnumerable<System.Collections.Generic.IReadOnlySet<(int X, int Y)>>;

namespace AdventOfCode.Year2023;

[Description("Point of Incidence")]
public class Day13 : IPuzzle
{
    public object Part1(string input)
        => Solve(input, 0);

    public object Part2(string input)
        => Solve(input, 1);

    private int Solve(string input, int i) =>
        ParsePatterns(input).Sum(pattern => SolvePattern(pattern, i));

    private static int SolvePattern(Pattern pattern, int smudge) =>
        SolveAxis(pattern, p => p.X, (p, v) => (v, p.Y), smudge) +
        SolveAxis(pattern, p => p.Y, (p, v) => (p.X, v), smudge) * 100;

    private static int SolveAxis(Pattern pattern, Func<Point, int> axisFunc, Func<Point, int, Point> pointSelector, int smudge) =>
        Enumerable.Range(1, pattern.Max(axisFunc))
            .Where(i => Enumerable.Range(0, Math.Min(i, pattern.Max(axisFunc) + 1 - i))
                .Sum(j =>  CountNonMatchingMirroredPoints(pattern, i, j, axisFunc, pointSelector)) == smudge)
            .Sum();

    private static int CountNonMatchingMirroredPoints(Pattern pattern, int i, int j, Func<Point, int> axisPointFunc, Func<Point, int, Point> pointSelector) =>
        NonMatchingMirroredPoints(pattern, axisPointFunc, pointSelector, i + j, i - j - 1)
        + NonMatchingMirroredPoints(pattern, axisPointFunc, pointSelector, i - j - 1, i + j);

    private static int NonMatchingMirroredPoints(Pattern pattern, Func<Point, int> axisPointFunc, Func<Point, int, Point> pointSelector, int axisPoint, int axisPointMirror) =>
        pattern
            .Where(p => axisPointFunc(p) == axisPoint)
            .Count(p => !pattern.Contains(pointSelector(p, axisPointMirror)));

    private static Patterns ParsePatterns(string input) =>
        input.ToLines()
            .ChunkBy(string.IsNullOrEmpty, false)
            .Select(ParsePattern);

    private static Pattern ParsePattern(IEnumerable<string> block) =>
        block.Select(ParseLine)
            .SelectMany(x => x)
            .ToHashSet();

    private static IEnumerable<Point> ParseLine(string line, int y) =>
        line.Select((c, x) => (c, x)).Where(t => t.c == '#').Select(t => (t.x, y));
}