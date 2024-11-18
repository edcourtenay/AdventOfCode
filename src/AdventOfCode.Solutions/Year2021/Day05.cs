using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2021;

[Description("Hydrothermal Venture")]
public partial class Day05 : IPuzzle
{
    public object Part1(string input)
    {
        return ParseData(input.ToLines())
            .Where(d => d.P1.X == d.P2.X || d.P1.Y == d.P2.Y)
            .SelectMany(ToPoints)
            .GroupBy(p => p)
            .Count(g => g.Count() > 1);
    }

    public object Part2(string input)
    {
        return ParseData(input.ToLines())
            .SelectMany(ToPoints)
            .GroupBy(p => p)
            .Count(g => g.Count() > 1);
    }

    public IEnumerable<Point> ToPoints(Line line)
    {
        var (x, y) = line.P1;
        var dx = Math.Sign(line.P2.X - line.P1.X);
        var dy = Math.Sign(line.P2.Y - line.P1.Y);

        while (x != line.P2.X + dx || y != line.P2.Y + dy)
        {
            yield return new Point(x, y);
            x += dx;
            y += dy;
        }
    }

    private static IEnumerable<Line> ParseData(IEnumerable<string> data)
    {
        return data
            .Select(d =>
            {
                var matchCollection = MyRegex().Matches(d);
                return new Line
                {
                    P1 = new Point(Convert.ToInt32(matchCollection[0].Groups[1].Value), Convert.ToInt32(matchCollection[0].Groups[2].Value)),
                    P2 = new Point(Convert.ToInt32(matchCollection[0].Groups[3].Value), Convert.ToInt32(matchCollection[0].Groups[4].Value))
                };
            });
    }

    public record struct Point(int X, int Y);
    public record struct Line(Point P1, Point P2);

    [GeneratedRegex(@"(\d+)\,(\d+)\s->\s(\d+)\,(\d+)", RegexOptions.Compiled)]
    private static partial Regex MyRegex();
}