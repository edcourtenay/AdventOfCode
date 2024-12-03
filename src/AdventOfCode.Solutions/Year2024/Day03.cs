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
        var span = input.AsSpan();
        var matches = Part2Regex().EnumerateMatches(span);

        int sum = 0;
        bool enabled = true;
        foreach (ValueMatch match in matches)
        {
            ReadOnlySpan<char> slice = span.Slice(match.Index, match.Length);
            switch (slice)
            {
                case ['d', 'o', ..]:
                    enabled = slice[2] is '(';
                    break;
                case ['m', 'u', 'l', ..] when enabled:
                    ReadOnlySpan<char> inner = slice[4..^1];
                    var spanSplitEnumerator = inner.Split(',');

                    var mul = 1;
                    while (spanSplitEnumerator.MoveNext())
                    {
                        mul *= int.Parse(inner[spanSplitEnumerator.Current]);
                    }
                    
                    sum += mul;
                    break;
            }
        }
        
        return sum;
    }

    [GeneratedRegex(@"mul\((?'a'\d{1,3}),(?'b'\d{1,3})\)")]
    private static partial Regex Part1Regex();

    [GeneratedRegex(@"mul\(\d{1,3},\d{1,3}\)|do\(\)|don\'t\(\)", RegexOptions.ExplicitCapture)]
    private static partial Regex Part2Regex();
}
