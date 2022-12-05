using System.Text.RegularExpressions;

namespace AdventOfCode.Year2022;

[Description("Supply Stacks")]
public partial class Day05 : IPuzzle
{
    public object Part1(string input) => ParseInput(input, Part1Strategy);

    public object Part2(string input) => ParseInput(input, Part2Strategy);

    public static string ParseInput(string input, Action<Stack<char>[], int, int, int> movementStrategy)
    {
        var dataSection = input.ToLines()
            .ToSequences(string.IsNullOrEmpty).GetEnumerator();

        dataSection.MoveNext();
        var stacks = ParseStacks(dataSection.Current);

        dataSection.MoveNext();
        ExecuteInstructions(dataSection, stacks, movementStrategy);

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

    private static void ExecuteInstructions(IEnumerator<IEnumerable<string>> enumerable, Stack<char>[] stacks,
        Action<Stack<char>[], int, int, int> movementStrategy)
    {
        foreach (string instruction in enumerable.Current)
        {
            if (InstructionRegex().Match(instruction) is not { Success: true } match)
            {
                continue;
            }

            int quantity = int.Parse(match.Groups["quantity"].Value);
            int source = int.Parse(match.Groups["source"].Value) - 1;
            var destination = int.Parse(match.Groups["destination"].Value) - 1;
            movementStrategy(stacks, quantity, destination, source);
        }
    }

    private static void Part1Strategy(Stack<char>[] stacks, int quantity, int destination, int source)
    {
        for (int i = 0; i < quantity; i++)
        {
            stacks[destination].Push(stacks[source].Pop());
        }
    }

    private static void Part2Strategy(Stack<char>[] stacks, int quantity, int destination, int source)
    {
        var tempStack = new Stack<char>();
        for (int i = 0; i < quantity; i++)
        {
            tempStack.Push(stacks[source].Pop());
        }

        foreach (char c in tempStack)
        {
            stacks[destination].Push(c);
        }
    }

    [GeneratedRegex("move (?<quantity>\\d+) from (?<source>\\d+) to (?<destination>\\d+)", RegexOptions.Compiled)]
    private static partial Regex InstructionRegex();
}