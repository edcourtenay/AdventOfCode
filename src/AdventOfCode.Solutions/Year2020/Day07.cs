using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020;

[Description("Handy Haversacks")]
public partial class Day07 : IPuzzle
{
    [GeneratedRegex(@"^(?<bag>\w+\s\w+) bags contain(?<item> (?<quantity>\d+) (?<contains>\w+\s\w+) bags?,?)*", RegexOptions.Multiline)]
    private static partial Regex BagRegex();

    public object Part1(string input)
    {
        var lines = ParseInput(input);

        var linesHash = lines.ToLookup(x => x.Parent);


        return string.Empty;
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    public IEnumerable<(string Parent, string Child, int Quantity)> ParseInput(string input)
    {
        MatchCollection matchCollection = BagRegex().Matches(input);
        foreach (Match match in matchCollection)
        {
            string value = match.Groups["bag"].Value;

            List<(int, string)> items = [];
            for (int i = 0; i < match.Groups["item"].Captures.Count; i++)
            {
                var quantity = int.Parse(match.Groups["quantity"].Captures[i].Value);
                string child = match.Groups["contains"].Captures[i].Value;
                yield return (value, child, quantity);
            }
        }
    }
}