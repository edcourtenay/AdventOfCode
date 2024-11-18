namespace AdventOfCode.Solutions.Year2017;

[Description("High-Entropy Passphrases")]
public class Day04 : IPuzzle
{
    private static readonly Func<string, string> SortWord = word =>
        new string(word.ToCharArray().OrderBy(c => c).ToArray());

    public object Part1(string input)
    {
        return Enumerable
            .Count<string>(input
                .ToLines(), s => ValidateLine(s, word => word));
    }

    public object Part2(string input)
    {
        return Enumerable
            .Count<string>(input
                .ToLines(), s => ValidateLine(s, SortWord));
    }

    public bool ValidateLine(string input, Func<string, string> selector)
    {
        var words = input.Split(' ')
            .Select(selector)
            .ToArray();

        return words.Distinct().Count() == words.Length;
    }
}