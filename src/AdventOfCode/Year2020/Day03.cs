namespace AdventOfCode.Year2020;

[Description("Toboggan Trajectory")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var charsEnumerable = input.ToLines(s => s.ToCharArray()).ToArray();
        var locationContents = GetLocationContents(charsEnumerable, (3, 1)).ToArray();
        return locationContents.Count(c => c == '#');
    }

    public object Part2(string input)
    {
        var gradients = new[] { (1, 1), (3, 1), (5, 1), (7, 1), (1, 2) };

        var charsEnumerable = input.ToLines(s => s.ToCharArray()).ToArray();
        IEnumerable<long> enumerable = gradients.Select(g => (long)(GetLocationContents(charsEnumerable, g).Count(c => c == '#')));
        return enumerable
            .Aggregate((i, j) => i * j);

    }

    public IEnumerable<char> GetLocationContents(char[][] map, (int X, int Y) gradient)
    {
        var x = 0;
        var skipLines = 0;
        foreach (char[] line in map)
        {
            if (skipLines > 0)
            {
                skipLines--;
                continue;
            }
            yield return line[x];
            x = (x + gradient.X) % line.Length;
            skipLines = gradient.Y - 1;
        }
    }
}