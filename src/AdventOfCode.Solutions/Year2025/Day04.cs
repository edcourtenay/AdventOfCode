namespace AdventOfCode.Solutions.Year2025;

[Description("Printing Department")]
public class Day04 : IPuzzle
{
    public object Part1(string input)
    {
        var count = 0;
        GetRemovablePoints(ParseGrid(input), (_, _) => count++);

        return count;
    }

    public object Part2(string input)
    {
        var grid = ParseGrid(input);

        var count = 0;
        int previousCount;
        do
        {
            previousCount = count;
            GetRemovablePoints(grid, (g, p) =>
            {
                g.Remove(p);
                count++;
            });
        } while (count != previousCount);

        return count;
    }

    private static void GetRemovablePoints(HashSet<Point> grid, Action<HashSet<Point>, Point> callback)
    {
        IEnumerable<Point> enumerable = grid
            .Where(p => Direction.Compass.Count(d => grid.Contains(p + d)) < 4);

        foreach (Point p in enumerable)
        {
            callback(grid, p);
        }
    }

    private static HashSet<Point> ParseGrid(string input)
    {
        var set = new HashSet<Point>();
        foreach ((int Y, string Item) line in input.ToLines().Index())
        {
            foreach ((int X, char Item) c in line.Item.Index())
            {
                if (c.Item == '@')
                {
                    set.Add(new Point(c.X, line.Y));
                }
            }
        }

        return set;
    }
}