namespace AdventOfCode.Solutions.Year2017;

[Description("Spiral Memory")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var sideLength = LengthOfSideWith(int.Parse(input));
        var midpoints = MidpointsForSideLength(sideLength);
        var stepsToRingFromCenter = (sideLength - 1) / 2;
        return stepsToRingFromCenter + midpoints.Select(midpoint => Math.Abs(int.Parse(input) - midpoint)).Min();
    }

    public object Part2(string input)
    {
        var memory = new SpiralMemory();
        var position = memory.AccessPort;
        var current = memory.Get(position);
        while (current < int.Parse(input))
        {
            position = memory.GetNext(position);
            current = memory.GetSumOfAdjacent(position);
            memory.Set(position, current);
        }
        return current;
    }

    private static int LengthOfSideWith(int n)
    {
        int i = (int)Math.Round(Math.Sqrt(n));
        return (i % 2 == 0) switch
        {
            true => i + 1,
            false => i
        };
    }

    private int[] MidpointsForSideLength(int sideLength)
    {
        var highest = sideLength * sideLength;
        var offset = (int)((sideLength - 1) / 2.0);
        return Enumerable.Range(0, 4)
            .Select(i => highest - (offset + (i * (sideLength - 1))))
            .ToArray();
    }

    class SpiralMemory
    {
        private readonly Dictionary<Point, int> _memory = new()
        {
            { Point.Zero, 1 }
        };

        public Point AccessPort => Point.Zero;

        public int Get(int x, int y) => Get((x, y));

        public int Get(Point position)
        {
            return _memory.GetValueOrDefault(position, 0);
        }

        public void Set(Point position, int value)
        {
            _memory[position] = value;
        }

        public int GetSumOfAdjacent(Point position)
        {
            return Adjacent(position).Select(Get).Sum();

            IEnumerable<Point> Adjacent(Point p)
            {
                foreach (var x in Enumerable.Range(p.X - 1, 3))
                {
                    foreach (int y in Enumerable.Range(p.Y - 1, 3))
                    {
                        if (x == p.X && y == p.Y)
                        {
                            continue;
                        }

                        yield return (x, y);
                    }
                }
            }
        }

        public Point GetNext(Point position)
        {
            var (x, y) = position;
            if (Get(x-1, y) > 0 && Get(x, y+1) == 0) return (x, y+1);
            if (Get(x, y-1) > 0 && Get(x-1, y) == 0) return (x-1, y);
            if (Get(x+1, y) > 0 && Get(x, y-1) == 0) return (x, y-1);
            return (x+1, y);
        }
    }
}