using System.CommandLine;
using System.Diagnostics;
using System.Reflection;
using System.Text.RegularExpressions;

using AdventOfCode;

using Spectre.Console;

var yearOption = new Option<int>(
    name: "--year",
    description: "The year of Advent of Code puzzles to run",
    getDefaultValue: () => DateTime.Today switch
    {
        { Month: >= 1 and <= 11, Year: var year } d => year - 1,
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

        AnsiConsole.MarkupLine($"[bold]{year:0000} Day {day:00}[/]: [link=https://adventofcode.com/{year}/day/{day}][dim]{description}[/][/]");
        AnsiConsole.MarkupLine(Run(puzzle, "Part 1", input, (p, s) => p.Part1(s)));
        AnsiConsole.MarkupLine(Run(puzzle, "Part 2", input, (p, s) => p.Part2(s)));
    }
}

static string Run(IPuzzle puzzle, string part, string input, Func<IPuzzle, string, object> func)
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
    return $"\t[bold]{part}[/]: [[[{timeColour}]{sw.Elapsed:mm\\:ss\\.fff}[/]]] [{resultColour}]{result}[/]";
}

static string ResourceString(int year, int day)
{
    var assembly = typeof(Program).GetTypeInfo().Assembly;
    using Stream manifestResourceStream = assembly.GetManifestResourceStream($"AdventOfCode.Input.Year{year:0000}.Day{day:00}.txt")!;
    using StreamReader reader = new(manifestResourceStream);

    return reader.ReadToEnd();
}

partial class Program
{
    [GeneratedRegex(@"Year(?<year>\d+)\.Day(?<day>\d+)")]
    private static partial Regex YearDayRegex();
}