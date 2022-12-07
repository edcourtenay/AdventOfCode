using System.Text.RegularExpressions;

namespace AdventOfCode.Year2016;

[Description("Internet Protocol Version 7")]
public partial class Day07 : IPuzzle
{
    public object Part1(string input)
    {
        return input.ToLines()
            .Count(SupportsTls);
    }

    public object Part2(string input)
    {
        return input.ToLines()
            .Count(SupportsSsl);
    }

    public static bool SupportsTls(string line)
    {
        var arr = line.Split('[', ']');

        return arr.Where((s, i) => i % 2 == 0).Any(HasAbba)
               && !arr.Where((s, i) => i % 2 != 0).Any(HasAbba);
    }

    public static bool HasAbba(string text)
    {
        return text.SlidingWindow(4)
            .Select(chars => chars.ToArray())
            .Any(chars => chars[0] == chars[3] && chars[1] == chars[2]
                && chars[0] != chars[1]);
    }

    public static bool SupportsSsl(string line)
    {
        var arr = line.Split('[', ']');

        var x = arr.Where((s, i) => i % 2 == 0)
            .SelectMany(FindAba)
            .Select(chars =>
            {
                chars[0] = chars[1];
                chars[1] = chars[2];
                chars[2] = chars[0];

                return new string(chars);
            });

        return x.Any(z => arr.Where((s, i) => i % 2 != 0).Any(v => v.Contains(z)));
    }

    public static IEnumerable<char[]> FindAba(string input)
    {
        return input.SlidingWindow(3)
            .Select(chars => chars.ToArray())
            .Where(chars => chars[0] == chars[2] && chars[0] != chars[1]);
    }

}