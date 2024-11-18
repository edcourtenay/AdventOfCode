namespace AdventOfCode.Solutions.Year2021;

[Description("Seven Segment Search")]
public class Day08 : IPuzzle
{
    public object Part1(string input)
    {
        var lengths = new[] { 2, 4, 3, 7 };

        return Enumerable.Select<string, (string[] observation, string[] digit)>(input.ToLines(), ParseLine)
            .SelectMany(tuple => tuple.digit)
            .Count(s => lengths.Contains(s.Length));
    }

    public object Part2(string input)
    {
        return Enumerable
            .Select<string, int>(input.ToLines(), DecodeLine)
            .Sum();
    }

    private readonly string[] Digits =
    [
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
    ];

    private static int DecodeLine(string line)
    {
        var parsedLine = ParseLine(line);

        var one = parsedLine.observation.Single(x => x.Length == 2);
        var four = parsedLine.observation.Single(x => x.Length == 4);
        var seven = parsedLine.observation.Single(x => x.Length == 3);
        var eight = parsedLine.observation.Single(x => x.Length == 7);
        var nine = parsedLine.observation.Single(x => x.Length == 6 && x.Except(seven).Except(four).Count() == 1);
        var six = parsedLine.observation.Single(x => x.Length == 6 && x != nine && one.Except(x).Count() == 1);
        var zero = parsedLine.observation.Single(x => x.Length == 6 && x != nine && x != six);

        var e = eight.Except(nine).Single();
        var c = eight.Except(six).Single();
        var f = one.Except([c]).Single();

        var five = parsedLine.observation.Single(x => x.Length == 5 && !x.Contains(c) && !x.Contains(e));
        var two = parsedLine.observation.Single(x => x.Length == 5 && x != five && x.Contains(c) && !x.Contains(f));
        var three = parsedLine.observation.Single(x => x.Length == 5 && x != five && x != two);

        var numbers = new[]
        {
            zero,
            one,
            two,
            three,
            four,
            five,
            six,
            seven,
            eight,
            nine
        };

        return parsedLine.digit
            .Select(x => numbers
                .Select((item, index) => (item, index))
                .Single(n => n.item.Length == x.Length && !n.item.Except(x).Any() && !x.Except(n.item).Any())
                .index)
            .Aggregate(0, (i, n) => i * 10 + n);
    }

    private static (string[] observation, string[] digit) ParseLine(string line)
    {
        var split = line.Split('|', StringSplitOptions.RemoveEmptyEntries);
        var observation = split[0].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var digit = split[1].Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return (observation, digit);
    }
}