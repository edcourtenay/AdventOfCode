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

    private static int Process(string input, bool useDampener = false)
    {
        var data = input.ToLines()
            .Select(i => i.Split(" ", StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray());

        return data.Select(line => Variants(line, useDampener)
                .Any(ints => ProcessLine(ints)))
            .Count(result => result);
    }

    private static bool ProcessLine(ReadOnlySpan<int> line)
    {
        var first = true;
        int slope = 0;
        bool safe = true;
        for (int i = 0; i < line.Length - 1; i++)
        {
            var a = line[i];
            var b = line[i + 1];
            var direction = b.CompareTo(a);

            if (first)
            {
                slope = direction;
                first = false;
            }

            if (Math.Abs(b - a) is >= 1 and <= 3 && slope == direction)
            {
                continue;
            }

            safe = false;
            break;
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
