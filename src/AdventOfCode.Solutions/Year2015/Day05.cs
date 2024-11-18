using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Solutions.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("Doesn't He Have Intern-Elves For This?")]
public class Day05 : IPuzzle
{
    private const string Vowels = "aeiou";

    public object Part1(string input)
    {
        return Data(input).Count(s => NiceTest(s));
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    public IEnumerable<string> Data(string input)
    {
        using var reader = new StringReader(input);

        while (reader.ReadLine() is { } line)
        {
            yield return line;
        }
    }

    public static readonly Func<string, bool> HasThreeVowels = text => text
        .Select(c => Vowels.Contains(c) ? 1 : 0).Sum() >= 3;

    public static readonly Func<string, bool> HasRepeatedLetter = text =>
    {
        char? last = null;
        using var enumerator = text.GetEnumerator();

        while (enumerator.MoveNext())
        {
            if (last.HasValue)
            {
                if (enumerator.Current == last)
                    return true;
            }

            last = enumerator.Current;
        }

        return false;
    };

    public static readonly Func<string, bool> HasDisallowedSubstring = text =>
    {
        var subs = new[] { "ab", "cd", "pq", "xy" };

        return subs.Any(text.Contains);
    };

    public static readonly Func<string, bool> NiceTest = text =>
        HasThreeVowels(text) && 
        HasRepeatedLetter(text) && 
        !HasDisallowedSubstring(text);
}