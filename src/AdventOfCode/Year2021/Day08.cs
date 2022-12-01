namespace AdventOfCode.Year2021;

[Description("Seven Segment Search")]
public class Day08 : IPuzzle
{
    public object Part1(string input)
    {
        return ExecutePart1(input.ToLines());
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    private readonly string[] Digits = {
        "abcefg", //0
        "cf", //1
        "acdeg", //2
        "acdfg", //3
        "bcdf", //4
        "abdfg", //5
        "abdefg", //6
        "acf", //7
        "abcdefg", //8
        "abcdfg"  //9
    };

    public long ExecutePart1(IEnumerable<string> data)
    {
        var lengths = new[] { 2, 4, 3, 7 };

        var lines = data.Select(ParseLine);

        return lines
            .SelectMany(tuple => tuple.digit)
            .Count(s => lengths.Contains(s.Length));
    }

    public (string[] observation, string[] digit) ParseLine(string line)
    {
        var split = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
        var observation = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var digit = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return (observation, digit);
    }
}