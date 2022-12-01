namespace AdventOfCode.Year2021;

[Description("Binary Diagnostic")]
public class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        int MostCommon((int zeroes, int ones) tuple) => tuple.ones > tuple.zeroes ? 1 : 0;
        int LeastCommon((int zeroes, int ones) tuple) => tuple.ones < tuple.zeroes ? 1 : 0;

        var lines = input
            .ToLines(d => d.ToArray())
            .Pivot()
            .Select(line => line.Aggregate((zeroes: 0, ones: 0), (current, c) => c switch
            {
                '0' => current with { zeroes = current.zeroes + 1 },
                '1' => current with { ones = current.ones + 1 },
                _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
            }))
            .ToArray();

        var epsilon = lines.Select(MostCommon).Aggregate(0, (i, j) => i * 2 + j);
        var gamma = lines.Select(LeastCommon).Aggregate(0, (i, j) => i * 2 + j);

        return gamma * epsilon;
    }

    public object Part2(string input)
    {
        return string.Empty;
    }
}