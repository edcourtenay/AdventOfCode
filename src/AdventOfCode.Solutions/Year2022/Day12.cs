namespace AdventOfCode.Solutions.Year2022;

[Description("Hill Climbing Algorithm")]
public class Day12 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, 'S', 'E', (from, to) => from >= to - 1);
    }

    public object Part2(string input)
    {
        return Solve(input, 'E', 'a', (from, to) => from <= to + 1);
    }

    private static int Solve(string input, char startValue, char targetValue, Func<char, char, bool> rule)
    {
        var lines = input.ToLines();

        var path = Navigate(ToMap(lines), startValue, targetValue, rule);
        return StepsTaken(path).Count();
    }

    private static Position[][] ToMap(IEnumerable<string> lines)
    {
        return lines.Select(l => l.Select(x => new Position(x)).ToArray())
            .ToArray();
    }

    private static IEnumerable<Step> StepsTaken(Step? step)
    {
        var current = step?.Previous ?? null;
        while (current != null)
        {
            yield return current;
            current = current.Previous;
        }
    }

    private static Step? Navigate(Position[][] map, char startValue, char targetValue, Func<char, char, bool> rule)
    {
        Queue<Step> queue = new([FindStartStep(map, startValue)]);

        while (queue.Count != 0)
        {
            var current = queue.Dequeue();
            if (map[current.Y][current.X].Value == targetValue)
            {
                return current;
            }

            foreach (Step move in ValidMoves(map, current, rule))
            {
                move.Previous = current;
                queue.Enqueue(move);
            }

            map[current.Y][current.X].Visited = true;
        }

        return null;
    }

    private static IEnumerable<Step> ValidMoves(Position[][] map, Step current, Func<char, char, bool> rule)
    {
        Step[] possibleSteps =
        [
            new(current.X, current.Y - 1),
            new(current.X, current.Y + 1),
            new(current.X - 1, current.Y),
            new(current.X + 1, current.Y)
        ];

        return possibleSteps.Where(to => IsValidMove(map, current, to, rule));
    }

    private static Step FindStartStep(Position[][] map, char startValue)
    {
        for (int y = 0; y < map.Length; y++)
        {
            for (int x = 0; x < map[y].Length; x++)
            {
                if (map[y][x].Value == startValue)
                {
                    return new Step(x, y);
                }
            }
        }

        throw new ApplicationException();
    }

    private static bool IsValidMove(Position[][] map, Step from, Step to, Func<char, char, bool> rule)
    {
        (int x, int y) = to;
        if (y >= 0 && y < map.Length && x >= 0 && x < map[y].Length && !map[from.Y][from.X].Visited)
        {
            return rule(ReplaceChar(map[from.Y][from.X].Value), ReplaceChar(map[y][x].Value));
        }

        return false;

        char ReplaceChar(char c) => c switch
        {
            'S' => 'a',
            'E' => 'z',
            _ => c
        };
    }

    private record struct Position(char Value)
    {
        public bool Visited { get; set; }
    }

    public record Step(int X, int Y)
    {
        public Step? Previous { get; set; }
    }
}