namespace AdventOfCode.Year2021;

[Description("Smoke Basin")]
public class Day09 : IPuzzle
{
    public object Part1(string input)
    {
        int[,] grid = ParseGrid(input);

        return LowestPoints(grid)
            .Select(tuple => grid[tuple.X, tuple.Y] + 1)
            .Sum();
    }

    public object Part2(string input)
    {
        int[,] grid = ParseGrid(input);

        return LowestPoints(grid)
            .Select(tuple => FloodFill(tuple.X, tuple.Y, grid))
            .OrderDescending()
            .Take(3)
            .Aggregate((a, b) => a * b);
    }

    private int FloodFill(int x, int y, int[,] grid)
    {
        var queue = new Queue<(int X, int Y)>();
        var visited = new HashSet<(int X, int Y)>();
        queue.Enqueue((x, y));
        while (queue.Count > 0)
        {
            var location = queue.Dequeue();
            if (!visited.Add(location))
            {
                continue;
            }

            foreach ((int X, int Y) neighbour in Adjacent(location.X, location.Y, grid))
            {
                var n = grid[neighbour.X, neighbour.Y];
                if (n != 9 && n > grid[x, y])
                {
                    queue.Enqueue(neighbour);
                }
            }
        }

        return visited.Count;
    }

    private static int[,] ParseGrid(string input)
    {
        var lines = input.ToLines()
            .ToArray();

        var grid = new int[lines[0].Length, lines.Length];

        for (int y = 0; y < lines.Length; y++)
        {
            for (int x = 0; x < lines[y].Length; x++)
            {
                grid[x, y] = lines[y][x] - '0';
            }
        }

        return grid;
    }

    private static IEnumerable<(int X, int Y)> LowestPoints(int[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                if (IsLowestPoint(x, y, grid))
                {
                    yield return (x, y);
                }
            }
        }
    }

    private static bool IsLowestPoint(int x, int y, int[,] grid)
    {
        return Adjacent(x, y, grid).All(neighbour => grid[x, y] < grid[neighbour.X, neighbour.Y]);
    }

    private static IEnumerable<(int X, int Y)> Adjacent(int x, int y, int[,] grid)
    {
        int width = grid.GetLength(0);
        int height = grid.GetLength(1);
        foreach ((int X, int Y) neighbour in CardinalNeighbours(x, y))
        {
            if (neighbour.X < 0 || neighbour.X >= width || neighbour.Y < 0 || neighbour.Y >= height)
            {
                continue;
            }

            yield return neighbour;
        }
    }

    private static IEnumerable<(int X, int Y)> CardinalNeighbours(int x, int y)
    {
        yield return (x, y - 1);
        yield return (x - 1, y);
        yield return (x + 1, y);
        yield return (x, y + 1);
    }
}