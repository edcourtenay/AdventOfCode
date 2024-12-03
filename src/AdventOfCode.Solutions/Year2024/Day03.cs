using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2024;

[Description("Mull It Over")]
public partial class Day03 : IPuzzle
{
    public object Part1(string input)
    {
        var matches = Part1Regex().Matches(input);

        int sum = 0;
        foreach (Match match in matches)
        {
            sum += int.Parse(match.Groups["a"].Value) * int.Parse(match.Groups["b"].Value);
        }

        return sum;
    }

    public object Part2(string input)
    {
        var matches = Part2Regex().Matches(input);

        int sum = 0;
        bool enabled = true;
        foreach (Match match in matches)
        {
            switch (match.Groups["instruction"].Value)
            {
                case "do":
                    enabled = true;
                    break;
                
                case "don't":
                    enabled = false;
                    break;
                
                case "mul" when enabled:
                    sum += int.Parse(match.Groups["a"].Value) * int.Parse(match.Groups["b"].Value);
                    break;
            }
        }
        
        return sum;
    }

    [GeneratedRegex(@"mul\((?'a'\d{1,3}),(?'b'\d{1,3})\)")]
    private static partial Regex Part1Regex();

    [GeneratedRegex(@"(?'instruction'mul)\((?'a'\d{1,3}),(?'b'\d{1,3})\)|(?'instruction'do)\(\)|(?'instruction'don\'t)\(\)")]
    private static partial Regex Part2Regex();
}
