namespace AdventOfCode.Year2022;

[Description("Rucksack Reorganization")]
public class Day03 : IPuzzle
{
    public object Part1(string input) =>
        Execute(input, s => s.ToLines(SplitIntoCompartments));

    public object Part2(string input) =>
        Execute(input, s => input.ToLines().Window(3));

    private static int Execute(string input, Func<string, IEnumerable<IEnumerable<IEnumerable<char>>>> func) =>
        func(input)
            .Select(strings => strings.Aggregate((prev, current) => current.Intersect(prev)).Single())
            .Sum(c => c switch
            {
                >= 'a' and <= 'z' => c - 'a' + 1,
                >= 'A' and <= 'Z' => c - 'A' + 27,
                _ => throw new ArgumentOutOfRangeException(nameof(c), c, null)
            });

    private static string[] SplitIntoCompartments(string arg)
    {
        int compartmentLength = arg.Length / 2;
        return new [] { arg[..compartmentLength], arg[^compartmentLength..] };
    }
}