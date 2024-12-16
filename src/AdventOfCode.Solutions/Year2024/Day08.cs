namespace AdventOfCode.Solutions.Year2024;

[Description("Resonant Collinearity")]
public class Day08 : IPuzzle
{
    public object Part1(string input)
    {
        return Execute(input, (d, p1, p2, gridSize, set) =>
        {
            Point p3 = p2 + d;
            Point p4 = p1 - d;

            if (gridSize.Contains(p3))
            {
                set.Add(p3);
            }

            if (gridSize.Contains(p4)) {
                set.Add(p4);
            }
        });
    }

    public object Part2(string input)
    {
        return Execute(input, (d, p1, p2, gridSize, set) =>
        {
            do
            {
                set.Add(p1);
            } while (gridSize.Contains((p1 -= d)));

            do
            {
                set.Add(p2);
            } while (gridSize.Contains((p2 += d)));
        });
    }

    private static int Execute(string input, Action<Direction, Point, Point, GridSize, HashSet<Point>> process)
    {
        (GridSize gridSize, Dictionary<char, HashSet<Point>> antennas) = Parse(input);

        var set = new HashSet<Point>();

        foreach ((char _, HashSet<Point> points) in antennas)
        {
            foreach (var combination in points.Combinations(2))
            {
                if (combination.ToArray() is not [var p1, var p2])
                {
                    continue;
                }

                process(p2 - p1, p1, p2, gridSize, set);
            }
        }

        return set.Count;
    }

    private static (GridSize gridSize, Dictionary<char, HashSet<Point>> antennas) Parse(string input)
    {
        IEnumerable<(int y, string data)> lines = input.ToLines().Index();

        int maxX = 0;
        int maxY = 0;
        var antennas = new Dictionary<char, HashSet<Point>>();
        foreach ((int y, string data) in lines)
        {
            maxY = Math.Max(y, maxY);

            foreach ((int x, char c) in data.Index())
            {
                maxX = Math.Max(x, maxX);

                if (c == '.')
                {
                    continue;
                }

                var set = antennas.GetValueOrDefault(c, []);
                set.Add(new Point(x, y));
                antennas[c] = set;
            }
        }

        return (new GridSize(maxX + 1, maxY + 1), antennas);
    }

    private readonly record struct Point(int X, int Y)
    {
        public static Direction operator -(Point p1, Point p2) => new(p1.X - p2.X, p1.Y - p2.Y);
        public static Point operator +(Point p, Direction d) => new(p.X + d.X, p.Y + d.Y);
        public static Point operator -(Point p, Direction d) => new(p.X - d.X, p.Y - d.Y);
    }

    private readonly record struct Direction(int X, int Y);

    private readonly record struct GridSize(int Width, int Height)
    {
        public bool Contains(Point point) =>
            point is { X: >= 0, Y: >= 0 } && point.X < Width && point.Y < Height;
    }
}
