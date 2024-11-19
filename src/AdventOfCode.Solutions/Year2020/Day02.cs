using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2020;

[Description("Password Philosophy")]
public partial class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, IsValidForPart1);
    }

    public object Part2(string input)
    {
        return Solve(input, IsValidForPart2);
    }

    private static object Solve(string input, Func<Item, bool> validation)
    {
        return input.ToLines(Item.Parse).Count(validation);
    }

    private static bool IsValidForPart1(Item item)
    {
        int count = item.Password.Count(c => c == item.Character);
        return count >= item.Min && count <= item.Max;
    }

    private static bool IsValidForPart2(Item item)
    {
        return (item.Password[item.Min - 1] == item.Character || item.Password[item.Max - 1] == item.Character)
               && item.Password[item.Min - 1] != item.Password[item.Max - 1];
    }

    [GeneratedRegex(@"(?<min>\d+)-(?<max>\d+) (?<char>\w): (?<password>\w+)", RegexOptions.Compiled)]
    private static partial Regex MyRegex();


    private record Item(int Min, int Max, char Character, string Password)
    {
        public static Item Parse(string input)
        {
            Match match = MyRegex().Match(input);

            return new Item(
                int.Parse(match.Groups["min"].Value),
                int.Parse(match.Groups["max"].Value),
                match.Groups["char"].Value[0],
                match.Groups["password"].Value);
        }
    }
}