namespace AdventOfCode.Solutions.Year2017;

[Description("Corruption Checksum")]
public class Day02 : IPuzzle {
    private static readonly Func<(int Min, int Max), int> Subtract = line => line.Max - line.Min;
    private static readonly Func<(int Min, int Max), int> Divide = line => line.Max / line.Min;

    public object Part1(string input)
    {
        return Process(input, line => line.MinMax(), Subtract);
    }

    public object Part2(string input)
    {
        return Process(input, FindDivisiblePair, Divide);
    }

    private static int Process(string input, Func<int[], (int Min, int Max)> selector, Func<(int Min, int Max), int> func)
    {
        return ParseInput(input)
            .Select(selector)
            .Select(func)
            .Sum();
    }

    private (int Min, int Max) FindDivisiblePair(int[] arg)
    {
        for (int i = 0; i < arg.Length; i++)
        {
            for (int j = 0; j < arg.Length; j++)
            {
                if (i == j)
                    continue;

                if (arg[i] % arg[j] == 0)
                    return (arg[j], arg[i]);
            }
        }

        throw new InvalidOperationException("No divisible pair found");
    }

    private static IEnumerable<int[]> ParseInput(string input)
    {
        return input.ToLines()
            .Select<string, int[]>(line => line.Split('\t')
                .Select(int.Parse).ToArray());
    }
}