namespace AdventOfCode.Solutions.Year2024;

[Description("Hoof It")]
public class Day10 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, x => x.Distinct().Count());
    }

    public object Part2(string input)
    {
        return Solve(input, x => x.Length);
    }

    private static long Solve(string input, Func<Point[], long> selector)
    {
        List<Point[]> lists = Execute(input);
        return lists.Sum(selector);
    }

    private static List<Point[]> Execute(string input)
    {
        List<List<int>> lines =
        [
            ..input.Split(Environment.NewLine)
                .Select(y => y.Select(x => int.Parse(x.ToString())).ToList())
        ];

        return lines.SelectMany((a, y) => a.Select((b, x) => (Position: new Point(x, y), Height: b)))
            .Where(c => c.Height == 0)
            .Select(x => Path(lines, [x.Position], [], x.Position))
            .ToList();
    }

    private static Point[] Path(List<List<int>> lines, Point[] pathSoFar, List<Point> trailEndPoints, Point startPos)
    {
        Stack<(Point CurrentPos, Point[] PathSoFar)> stack = new();
        stack.Push((startPos, pathSoFar));

        while (stack.Count > 0)
        {
            (Point pos, Point[] currentPath) = stack.Pop();

            if (lines[pos.Y][pos.X] == 9)
            {
                trailEndPoints.Add(pos);
            }
            else
            {
                foreach (Direction direction in Direction.Orthogonal)
                {
                    Point newPosition = pos + direction;

                    if (newPosition.X >= 0 &&
                        newPosition.X < lines[0].Count &&
                        newPosition.Y >= 0 &&
                        newPosition.Y < lines.Count &&
                        lines[newPosition.Y][newPosition.X] - lines[pos.Y][pos.X] == 1 &&
                        !currentPath.Contains(newPosition))
                    {
                        stack.Push((newPosition, currentPath.Append(newPosition).ToArray()));
                    }
                }
            }
        }

        return trailEndPoints.ToArray();
    }
}