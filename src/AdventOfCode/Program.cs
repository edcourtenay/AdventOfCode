using System.Collections.Concurrent;
using System.CommandLine;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

using AdventOfCode;

using Spectre.Console;

using static System.Text.Json.JsonSerializer;

var yearOption = new Option<int>(
    name: "--year",
    description: "The year of Advent of Code puzzles to run",
    getDefaultValue: () => DateTime.Today switch
    {
        { Month: >= 1 and <= 11, Year: var year } => year - 1,
        { Year: var year } => year
    });

var dayOption = new Option<int?>(
    name: "--day",
    description: "Specific day to run");

var rootCommand = new RootCommand("Advent of Code solution runner");
rootCommand.AddOption(yearOption);
rootCommand.AddOption(dayOption);
rootCommand.SetHandler(ExecutePuzzles, yearOption, dayOption);

return await rootCommand.InvokeAsync(args);

static void ExecutePuzzles(int selectedYear, int? selectedDay)
{
    ConcurrentDictionary<int, Dictionary<int, string[]>?> yearResults = new();

    var puzzles = typeof(Program).Assembly
        .GetTypes()
        .Where(t => typeof(IPuzzle).IsAssignableFrom(t))
        .Where(t => t is { IsInterface: false, IsAbstract: false })
        .Select(t => (type: t, match: YearDayRegex().Match(t.FullName!)))
        .Where(t => t.match.Success)
        .Select(t => (t.type, year: int.Parse(t.match.Groups["year"].Value), day: int.Parse(t.match.Groups["day"].Value)))
        .OrderBy(t => t.year)
        .ThenBy(t => t.day)
        .Where(t => t.year == selectedYear && ((selectedDay.HasValue && t.day == selectedDay.Value) || !selectedDay.HasValue));

    foreach ((Type puzzleType, int year, int day) in puzzles)
    {
        if (Activator.CreateInstance(puzzleType) is not IPuzzle puzzle)
            continue;

        string description = puzzleType.GetCustomAttribute<DescriptionAttribute>() is { } descriptionAttribute
            ? descriptionAttribute.Description
            : "Unknown";
        string input = ResourceString(year, day);
        var results = yearResults.GetOrAdd(year, Results);
        string[]? strings = null;
        if (results != null && results.ContainsKey(day))
        {
            strings = results[day];
        }

        var result1 = strings is [{ } r1, ..] ? r1 : null;
        var result2 = strings is [_, {} r2] ? r2 : null;

        AnsiConsole.MarkupLine($"[bold]{year:0000} Day {day:00}[/]: [link=https://adventofcode.com/{year}/day/{day}][dim]{description}[/][/]");
        AnsiConsole.MarkupLine(Run(puzzle, "Part 1", input, (p, s) => p.Part1(s), result1));
        AnsiConsole.MarkupLine(Run(puzzle, "Part 2", input, (p, s) => p.Part2(s), result2));
    }
}

static string Run(IPuzzle puzzle, string part, string input, Func<IPuzzle, string, object> func, string? expectedResult)
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
    (string resultColour, object result) = obj switch
    {
        string s when string.IsNullOrEmpty(s) => ("red", "Incomplete"),
        _ => ("cyan", obj)
    };
    string checkOrCross = expectedResult switch
    {
        {} s when s == result.ToString() => "[green]✓[/]",
        {} s => "[red]×[/]",
        _ => "[purple]?[/]"
    };
    return $"\t[bold]{part}[/]: [[[{timeColour}]{sw.Elapsed:mm\\:ss\\.fff}[/]]] [{resultColour}]{result}[/] {checkOrCross}";
}

static string ResourceString(int year, int day)
{
    var assembly = typeof(Program).GetTypeInfo().Assembly;
    using Stream manifestResourceStream = assembly.GetManifestResourceStream($"AdventOfCode.Input.Year{year:0000}.Day{day:00}.txt")!;
    using StreamReader reader = new(manifestResourceStream);

    return reader.ReadToEnd();
}

static Dictionary<int, string[]>? Results(int year)
{
    var assembly = typeof(Program).GetTypeInfo().Assembly;
    using Stream? manifestResourceStream = assembly.GetManifestResourceStream($"AdventOfCode.Input.Year{year:0000}.results.json")!;
    return manifestResourceStream == null
        ? null
        : Deserialize<Dictionary<int, string[]>>(manifestResourceStream);
}

partial class Program
{
    [GeneratedRegex(@"Year(?<year>\d+)\.Day(?<day>\d+)")]
    private static partial Regex YearDayRegex();
}