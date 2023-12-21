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
        throw new NotImplementedException();
    }

    public int Solve(string input, int targetSteps)
    {
        return WalkGarden(Parse(input), targetSteps).Count;
    }

    public HashSet<Point> WalkGarden(Garden garden, int targetSteps)
    {
        var directions = new Point[]{(-1,0), (1,0), (0,1), (0,-1)};
        var reached = new HashSet<Point>();
        var visited = new HashSet<(Point point, int steps)>();
        var queue = new Queue<(Point point, int steps)>();

        queue.Enqueue((garden.start, 0));

        while(queue.TryDequeue(out var location))
        {
            if(location.steps == targetSteps)
            {
                reached.Add(location.point);
                continue;
            }

            foreach(var direction in directions)
            {
                var newX = location.point.x + direction.x;
                var newY = location.point.y + direction.y;

                if(newX < 0 || newX > garden.maxX || newY < 0 || newY > garden.maxY || garden.stones.Contains((newX, newY)))
                {
                    continue;
                }

                ((int newX, int newY) nextPoint, int) next = (nextPoint: (newX, newY), location.steps + 1);
                if (visited.Add(next))
                {
                    queue.Enqueue(next);
                }
            }
        }

        return reached;
    }

    public Garden Parse(string input)
    {
        var maxX = 0;
        var maxY = 0;

        Point start = (0, 0);
        var stones = new HashSet<Point>();
        var locations = input.ToLines().SelectMany((line, y) => line.Select((c, x) => (c, x, y)));

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

        return (stones, start, maxX, maxY);
    }
}