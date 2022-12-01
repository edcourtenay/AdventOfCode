using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

using AdventOfCode;

using Spectre.Console;

var dayRegex = MyRegex();

var types = typeof(Program).Assembly
    .GetTypes();

var puzzles = types
    .Where(t => typeof(IPuzzle).IsAssignableFrom(t))
    .Where(t => !t.IsInterface)
    .Where(t => !t.IsAbstract)
    .Where(t => t.FullName!.Split('.')[^2] == "Year2022")
    .OrderBy(t => t.FullName);

foreach (var puzzleType in puzzles)
{
    if (Activator.CreateInstance(puzzleType) is not IPuzzle puzzle)
        continue;

    string name = ToDisplay(puzzleType.Name);
    string description = puzzleType.GetCustomAttribute<DescriptionAttribute>() is { } descriptionAttribute
        ? descriptionAttribute.Description
        : "Unknown";
    string input = ResourceString(puzzleType);
    
    AnsiConsole.MarkupLine($"[bold]{name}[/]: [dim]{description}[/]");
    AnsiConsole.MarkupLine(Run(puzzle, "Part 1", input, (p, s) => p.Part1(s)));
    AnsiConsole.MarkupLine(Run(puzzle, "Part 2", input, (p, s) => p.Part2(s)));
}

string Run(IPuzzle puzzle, string part, string input, Func<IPuzzle, string, object> func)
{
    var sw = Stopwatch.StartNew();
    var obj = func(puzzle, input);
    sw.Stop();
    string timeColour = sw.ElapsedMilliseconds switch
    {
        > 1000 => "red",
        > 500 => "yellow",
        _ => "green"
    };
    return $"\t[bold]{part}[/]: [[[{timeColour}]{sw.Elapsed:mm\\:ss\\.fff}[/]]] {obj}";
}
    
string ToDisplay(string puzzleTypeName)
{
    return dayRegex.Match(puzzleTypeName) is { Success: true } match
        ? $@"Day {match.Groups["day"].Value}"
        : puzzleTypeName;
}

string ResourceString(Type puzzleType)
{
    var assembly = typeof(Program).GetTypeInfo().Assembly;
    var year = puzzleType.Namespace?.Split('.')[^1];
    using Stream manifestResourceStream = assembly.GetManifestResourceStream($"AdventOfCode.Input.{year}.{puzzleType.Name}.txt")!;
    using StreamReader reader = new(manifestResourceStream);

    return reader.ReadToEnd();
}

partial class Program
{
    [GeneratedRegex(@"Day(?<day>\d+)")]
    private static partial Regex MyRegex();
}