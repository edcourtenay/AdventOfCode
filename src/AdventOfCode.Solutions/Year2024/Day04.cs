namespace AdventOfCode.Solutions.Year2024;

[Description("Ceres Search")]
public class Day04 : IPuzzle
{
    private readonly List<(int dx, int dy)[]> _part1SearchDirections =
    [
        [(0, -1), (0, -2), (0, -3)],
        [(0, 1), (0, 2), (0, 3)],
        [(-1, 0), (-2, 0), (-3, 0)],
        [(1, 0), (2, 0), (3, 0)],
        [(1, 1), (2, 2), (3, 3)],
        [(-1, -1), (-2, -2), (-3, -3)],
        [(1, -1), (2, -2), (3, -3)],
        [(-1, 1), (-2, 2), (-3, 3)]
    ];

    private readonly List<(int dx, int dy)[]> _part2SearchDirections =
    [
        [(1, 1), (-1, -1)],
        [(-1, -1), (1, 1)],
        [(1, -1), (-1, 1)],
        [(-1, 1), (1, -1)]
    ];

    public object Part1(string input)
    {
        var lines = input.ToLines()
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToArray();

        int sum = 0;
        for (int x = 0; x < lines.Length; x++)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                if (lines[x][y] != 'X')
                {
                    continue;
                }

                sum += _part1SearchDirections.Count(directions => 
                    GetAt(ref lines, x + directions[0].dx, y + directions[0].dy) == 'M'
                    && GetAt(ref lines, x + directions[1].dx, y + directions[1].dy) == 'A'
                    && GetAt(ref lines, x + directions[2].dx, y + directions[2].dy) == 'S');
            }
        }
        
        return sum;
    }

    public object Part2(string input)
    {
        var lines = input.ToLines()
            .Where(l => !string.IsNullOrWhiteSpace(l))
            .ToArray();

        int sum = 0;
        for (int x = 0; x < lines.Length; x++)
        {
            for (int y = 0; y < lines.Length; y++)
            {
                if (lines[x][y] != 'A')
                {
                    continue;
                }

                sum += _part2SearchDirections.Count(directions =>
                    GetAt(ref lines, x + directions[0].dx, y + directions[0].dy) == 'M'
                    && GetAt(ref lines, x + directions[1].dx, y + directions[1].dy) == 'S') == 2 ? 1 : 0;
            }
        }

        return sum;
    }

    private char GetAt(ref string[] ll, int x, int y)
    {
        if (x < 0 || x >= ll[0].Length || y < 0 || y >= ll.Length)
        {
            return '*';
        }

        return ll[x][y];
    }
}
