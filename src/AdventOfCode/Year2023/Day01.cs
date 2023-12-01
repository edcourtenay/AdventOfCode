using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023;

[Description("Trebuchet?!")]
public partial class Day01 : IPuzzle
{
    private const string NumbersExpression = "(?=(?'number'one|two|three|four|five|six|seven|eight|nine|\\d))";

    [GeneratedRegex(NumbersExpression, RegexOptions.Compiled )]
    private static partial Regex NumbersRegex();

    public object Part1(string input)
    {
        return input.ToLines(ParseLine1)
            .Sum();
    }

    public object Part2(string input)
    {
        return input.ToLines(ParseLine2)
            .Sum();
    }

    private int ParseLine1(string line)
    {
        return (line.First(char.IsAsciiDigit) - '0') * 10 + (line.Last(char.IsAsciiDigit) - '0');
    }

    private int ParseLine2(string line)
    {
        MatchCollection matchCollection = NumbersRegex().Matches(line);

        return MatchToInt(matchCollection[0]) * 10 + MatchToInt(matchCollection[^1]);
    }

    private int MatchToInt(Match match)
    {
        return match.Groups["number"].ValueSpan switch
        {
            "zero" or "0" => 0,
            "one" or "1" => 1,
            "two" or "2" => 2,
            "three" or "3" => 3,
            "four" or "4" => 4,
            "five" or "5" => 5,
            "six" or "6" => 6,
            "seven" or "7" => 7,
            "eight" or "8" => 8,
            "nine" or "9" => 9,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}