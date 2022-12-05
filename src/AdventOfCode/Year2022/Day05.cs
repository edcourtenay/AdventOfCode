using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022;

[Description("Puzzle Title")]
public class Day05 : IPuzzle
{
    private static Regex s_regex = new(@"move (?<quantity>\d+) from (?<source>\d+) to (?<destination>\d+)",
        RegexOptions.Compiled);


    public object Part1(string input) => ParseInput(input);

    public object Part2(string input) => string.Empty;

    public static string ParseInput(string input)
    {
        var enumerable = input.ToLines()
            .ToSequences(string.IsNullOrEmpty).GetEnumerator();

        enumerable.MoveNext();

        var example = new Stack<char>("12345");
        var ch = example.Pop();
        example.Push('x');

        var stacks = ParseStacks(enumerable.Current);

        enumerable.MoveNext();
        foreach (string instruction in enumerable.Current)
        {
            if (s_regex.Match(instruction) is not { Success: true } match)
            {
                continue;
            }

            for (int i = 0; i < int.Parse(match.Groups["quantity"].Value); i++)
            {
                var source = int.Parse(match.Groups["source"].Value) - 1;
                var destination = int.Parse(match.Groups["destination"].Value) - 1;

                stacks[destination].Push(stacks[source].Pop());
            }
        }

        return new string(stacks.Select(stack => stack.Pop()).ToArray());
    }

    private static Stack<char>[] ParseStacks(IEnumerable<string> strings)
    {
        var arr = strings.ToArray();
        var maxLength = arr.Max(s => s.Length);

        return arr.Select(s => s.PadRight(maxLength, ' '))
            .Pivot().Select(chars => new string(chars.Reverse().ToArray())).Where(s1 => s1.Length != 0 && char.IsDigit(s1[0]))
            .Select(s2 => new Stack<char>(s2.Trim()[1..])).ToArray();
    }
}