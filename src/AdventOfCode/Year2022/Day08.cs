namespace AdventOfCode.Year2022;

[Description("Treetop Tree House")]
public class Day08 : IPuzzle
{
    private static readonly (int x, int y)[] Directions = { (1, 0), (-1, 0), (0, 1), (0, -1) };

    public object Part1(string input)
    {
        var grid = ToGrid(input);

        return GridPositions(grid)
            .Count(tuple => IsVisible(grid, tuple.X, tuple.Y));
    }

    public object Part2(string input)
    {
        var grid = ToGrid(input);

        return GridPositions(grid)
            .Max(tuple => CalculateScenicScore(grid, tuple.X, tuple.Y));
    }

    private static int CalculateScenicScore(int[,] grid, int x, int y)
    {
        return Directions
            .Select(direction => ItemsInDirection(grid, direction, x, y).Skip(1).TakeUntil(i => i >= grid[x, y]).Count())
            .Aggregate((c1, c2) => c1 * c2);
    }

    private static int[,] ToGrid(string input)
    {
        int[,] grid = { };
        foreach ((string data, int y) in input.ToLines().Select((s, i) => (data: s, y: i )))
        {
            if (y == 0)
            {
                grid = new int[data.Length, data.Length];
            }
            foreach ((char ch, int x) in data.Select((c, i) => (c, x: i)))
            {
                grid[x, y] = ch - '0';
            }
        }

        return grid;
    }

    private static bool IsVisible(int[,] grid, int x, int y)
    {
        return Directions.Any(tuple => IsVisible(ItemsInDirection(grid, tuple, x, y)));
    }

    private static IEnumerable<int> ItemsInDirection(int[,] grid, (int dx, int dy) direction, int x, int y)
    {
        while (x >= 0 && x < grid.GetLength(0) && y >= 0 && y < grid.GetLength(1))
        {
            yield return grid[x, y];
            x += direction.dx;
            y += direction.dy;
        }
    }

    private static bool IsVisible(IEnumerable<int> items)
    {
        using IEnumerator<int> enumerator = items.GetEnumerator();
        enumerator.MoveNext();

        var source = enumerator.Current;
        while (enumerator.MoveNext())
        {
            if (enumerator.Current >= source)
                return false;
        }

        return true;
    }

    private static IEnumerable<(int X, int Y)> GridPositions(int[,] grid)
    {
        for (int y = 0; y < grid.GetLength(1); y++)
        {
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                yield return (x, y);
            }
        }
    }
}