using System.Text.RegularExpressions;

namespace AdventOfCode.Year2023;

[Description("Trebuchet?!")]
public partial class Day01 : IPuzzle
{
    private const string NumbersExpression = "zero|one|two|three|four|five|six|seven|eight|nine|\\d";

    [GeneratedRegex(NumbersExpression, RegexOptions.Compiled )]
    private static partial Regex LeftNumbersRegex();

    [GeneratedRegex(NumbersExpression, RegexOptions.Compiled | RegexOptions.RightToLeft )]
    private static partial Regex RightNumbersRegex();

    public object Part1(string input)
    {
        return input.ToLines(ParseLine)
            .Sum();
    }

    public object Part2(string input)
    {
        return input.ToLines(ParseLine2)
            .Sum();
    }

    private int ParseLine(string line)
    {
        return (line.First(char.IsAsciiDigit) - '0') * 10 + (line.Last(char.IsAsciiDigit) - '0');
    }

    private int ParseLine2(string line)
    {
        Match first = LeftNumbersRegex().Match(line);
        Match second = RightNumbersRegex().Match(line);

        return MatchToInt(first) * 10 + MatchToInt(second);
    }

    private int MatchToInt(Match match)
    {
        return match.Value switch
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