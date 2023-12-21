using Point = (int x, int y);
using Garden = (string[] map, (int x, int y) start, int size);

namespace AdventOfCode.Year2023;

[Description("Step Counter")]
public class Day21 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, 64);
    }

    public object Part2(string input)
    {
        const int target = 26501365;
        var garden = Parse(input);

        long grids = target / garden.size;
        var remaining = target % garden.size;

        int[] targets = Enumerable.Range(0, 3).Select(i => (i * garden.size) + remaining).ToArray();
        int[] results = WalkGarden(garden, targets);

        long[] deltas = [results[0], results[1] - results[0], results[2] - 2 * results[1] + results[0]];

        long answer = deltas[0] + deltas[1] * (grids) +
                      deltas[2] * (grids * (grids - 1) / 2);

        return answer;
    }

    public static int Solve(string input, int targetSteps)
    {
        return WalkGarden(Parse(input), [targetSteps]).First();
    }

    private static int[] WalkGarden(Garden garden, int[] targetSteps)
    {
        Point[] directions = [(-1, 0), (1, 0), (0, 1), (0, -1)];
        Dictionary<Point, int> visited = new();
        Queue<(Point point, int steps)> queue = new();
        int maxTarget = targetSteps.Max();

        queue.Enqueue((garden.start, 0));

        while(queue.TryDequeue(out var location))
        {
            if(location.steps == maxTarget)
            {
                continue;
            }

            foreach(var direction in directions)
            {
                var newX = location.point.x + direction.x;
                var newY = location.point.y + direction.y;

                int wrapX = ((newX % garden.size) + garden.size) % garden.size;
                int wrapY = ((newY % garden.size) + garden.size) % garden.size;

                if (garden.map[wrapY][wrapX] == '#')
                {
                    continue;
                }

                (Point point, int steps) next = ((newX, newY), location.steps + 1);
                if (visited.ContainsKey(next.point))
                {
                    continue;
                }

                visited.Add(next.point, next.steps);
                queue.Enqueue(next);
            }
        }

        return targetSteps
            .Select(i => visited.Count(kvp => kvp.Value <= i && (kvp.Value & 0x1) == (i & 0x1)))
            .ToArray();
    }

    private static Garden Parse(string input)
    {
        Point start = (0, 0);

        var grid = input.ToLines().ToArray();

        for (int y = 0; y < grid.Length; y++)
        {
            var line = grid[y];
            for (int x = 0; x < line.Length; x++)
            {
                if (line[x] != 'S')
                {
                    continue;
                }

                start = (x, y);
                break;
            }
        }

        return (grid, start, grid.Length);
    }
}