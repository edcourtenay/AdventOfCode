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
    .Select(t => (type: t, match: dayRegex.Match(t.FullName)))
    .Where(t => t.match.Success)
    .Select(t => (type: t.type, year: int.Parse(t.match.Groups["year"].Value), day: int.Parse(t.match.Groups["day"].Value)))
    .OrderBy(t => t.year)
    .ThenBy(t => t.day)
    .Where(t => t.year == 2022);

foreach (var (puzzleType, year, day) in puzzles)
{
    if (Activator.CreateInstance(puzzleType) is not IPuzzle puzzle)
        continue;

    string description = puzzleType.GetCustomAttribute<DescriptionAttribute>() is { } descriptionAttribute
        ? descriptionAttribute.Description
        : "Unknown";
    string input = ResourceString(year, day);
    
    AnsiConsole.MarkupLine($"[bold]{year:0000} Day {day:00}[/]: [dim]{description}[/]");
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

string ResourceString(int year, int day)
{
    var assembly = typeof(Program).GetTypeInfo().Assembly;
    using Stream manifestResourceStream = assembly.GetManifestResourceStream($"AdventOfCode.Input.Year{year:0000}.Day{day:00}.txt")!;
    using StreamReader reader = new(manifestResourceStream);

    return reader.ReadToEnd();
}

partial class Program
{
    [GeneratedRegex(@"Year(?<year>\d+)\.Day(?<day>\d+)")]
    private static partial Regex MyRegex();
}