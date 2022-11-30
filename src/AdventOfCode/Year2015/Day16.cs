using System.Text.RegularExpressions;

namespace AdventOfCode.Year2015;

[Description("Aunt Sue")]
public class Day16 : IPuzzle
{
    private readonly Regex _regexPair = new(@"(?<attribute>\w+):\s(?<value>\d+)", RegexOptions.Compiled);

    private static readonly Dictionary<string, int> Target = new Dictionary<string, int>
    {
        ["children"] = 3,
        ["cats"] = 7,
        ["samoyeds"] = 2,
        ["pomeranians"] = 3,
        ["akitas"] = 0,
        ["vizslas"] = 0,
        ["goldfish"] = 5,
        ["trees"] = 3,
        ["cars"] = 2,
        ["perfumes"] = 1,
    };

    public object Part1(string input)
    {
        return Parse(input).FindIndex(p => p.Keys.All(key => p[key] == Target[key])) + 1;
    }

    public object Part2(string input)
    {
        return Parse(input).FindIndex(p => p.Keys.All(key => key switch
        {
            "cats" or "trees" => p[key] > Target[key],
            "pomeranians" or "goldfish" => p[key] < Target[key],
            _ => p[key] == Target[key]
        })) + 1;
    }

    private IEnumerable<string> Data(string input)
    {
        var reader = new StringReader(input);
        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }

    public List<Dictionary<string, int>> Parse(string input)
    {
        return Data(input)
            .Select(s => _regexPair.Matches(s).ToDictionary(
                m => m.Groups["attribute"].Value,
                m => int.Parse(m.Groups["value"].Value)))
            .ToList();
    }
}