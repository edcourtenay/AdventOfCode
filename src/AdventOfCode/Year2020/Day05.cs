namespace AdventOfCode.Year2020;

[Description("Binary Boarding")]
public class Day05 : IPuzzle
{
    public object Part1(string input)
    {
        return input.ToLines(Parse)
            .Max();
    }

    public object Part2(string input)
    {
        return input.ToLines(Parse)
            .Order()
            .Pairwise()
            .First(tuple => tuple.First + 1 == tuple.Second - 1)
            .First + 1;
    }

    public static int Parse(string line)
    {
        var row = Process(line[..7], 127);
        var col = Process(line[^3..], 7);

        return row * 8 + col;
    }

    private static int Process(string data, int high)
    {
        return data.Aggregate((low: 0, high), (current, c) => c switch
        {
            'F' or 'L' => (current.low, current.high - (current.high - current.low) / 2 - 1),
            'B' or 'R' => (current.low + (current.high - current.low) / 2 + 1, current.high),
            _ => throw new ArgumentOutOfRangeException()
        }).low;
    }
}