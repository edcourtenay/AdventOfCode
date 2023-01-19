namespace AdventOfCode.Year2022;

[Description("Unstable Diffusion")]
public class Day23 : IPuzzle
{
    public object Part1(string input)
    {
        var set = new HashSet<(int x, int y)>(ParseData(input));

        return string.Empty;
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    private static IEnumerable<(int x, int y)> ParseData(string input)
    {
        int y = 0;
        foreach (string line in input.ToLines())
        {
            int x = 0;
            foreach (char ch in line)
            {
                if (ch == '#')
                {
                    yield return (x, y);
                }

                x++;
            }

            y++;
        }
    }
}