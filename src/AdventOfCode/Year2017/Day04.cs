namespace AdventOfCode.Year2017;

[Description("High-Entropy Passphrases")]
public class Day04 : IPuzzle
{
    public object Part1(string input)
    {
        return input.ToLines()
            .Where(ValidateLine)
            .Count();
    }

    public object Part2(string input) => string.Empty;

    public bool ValidateLine(string input)
    {
        var words = input.Split(' ');
        return words.Distinct().Count() == words.Length;
    }
}