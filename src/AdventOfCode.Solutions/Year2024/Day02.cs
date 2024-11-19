namespace AdventOfCode.Solutions.Year2024;

[Description("Red-Nosed Reports")]
public class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        return Process(input);
    }

    public object Part2(string input)
    {
        return Process(input, true);
    }

    private static object Process(string input, bool useDampener = false)
    {
        var data = input.ToLines()
            .Select(i => i.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());

        return data.Select(line => Variants(line, useDampener)
                .Any(ints => ProcessLine(ref ints)))
            .Count(result => result);
    }

    private static bool ProcessLine(ref int[] line)
    {
        var first = true;
        int slope = 0;
        bool safe = true;
        foreach (var window in line.SlidingWindow(2))
        {
            var pair = window.ToArray();
            var direction = pair[1].CompareTo(pair[0]);

            if (first)
            {
                slope = direction;
                first = false;
            }

            if (Math.Abs(pair[1] - pair[0]) is < 1 or > 3 || slope != direction)
            {
                safe = false;
                break;
            }
        }

        return safe;
    }

    private static IEnumerable<int[]> Variants(int[] line, bool useDampener = false)
    {
        yield return line;

        if (!useDampener)
        {
            yield break;
        }

        for (int i = 0; i < line.Length; i++)
        {
            yield return [..line[..i], ..line[(i + 1)..]];
        }
    }
}
