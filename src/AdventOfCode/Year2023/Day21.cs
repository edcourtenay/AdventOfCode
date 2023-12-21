using Point = (int x, int y);
using Garden = (System.Collections.Generic.IReadOnlySet<(int x, int y)> stones, (int x, int y) start, int maxX, int maxY);

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

        long grids = target / garden.maxX;
        var remaining = target % garden.maxX;

        int[] targets = Enumerable.Range(0, 3).Select(i => (i * garden.maxX) + remaining).ToArray();
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
        var directions = new Point[]{(-1,0), (1,0), (0,1), (0,-1)};
        var visited = new Dictionary<Point, int>();
        var queue = new Queue<(Point point, int steps)>();
        var maxTarget = targetSteps.Max();

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

                if (garden.stones.Contains((((newX % garden.maxX) + garden.maxX) % garden.maxX, ((newY % garden.maxY) + garden.maxY) % garden.maxY)))
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
        var maxX = 0;
        var maxY = 0;

        Point start = (0, 0);
        var stones = new HashSet<Point>();
        var locations = input.ToLines()
            .SelectMany((line, y) => line.Select((c, x) => (c, x, y)));

        foreach ((char c, int x, int y) location in locations)
        {
            switch (location.c)
            {
                case '#':
                    stones.Add((location.x, location.y));
                    break;

                case 'S':
                    start = (location.x, location.y);
                    break;
            }

            maxX = Math.Max(maxX, location.x);
            maxY = Math.Max(maxY, location.y);
        }

        return (stones, start, maxX +1,  maxY + 1);
    }
}