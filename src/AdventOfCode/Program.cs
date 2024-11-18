using System.Collections.Concurrent;
using System.CommandLine;
using System.Diagnostics;
using System.Reflection;

using AdventOfCode;
using AdventOfCode.Solutions;
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

var iterationsOption = new Option<int>(
    name: "--iterations",
    description: "Number of times to re-run puzzle (for timing purposes)",
    getDefaultValue: () => 1
);

var rootCommand = new RootCommand("Advent of Code solution runner");
rootCommand.AddOption(yearOption);
rootCommand.AddOption(dayOption);
rootCommand.AddOption(iterationsOption);
rootCommand.SetHandler(ExecutePuzzles, yearOption, dayOption, iterationsOption);

return await rootCommand.InvokeAsync(args);

static void ExecutePuzzles(int selectedYear, int? selectedDay, int iterations)
{
    ConcurrentDictionary<int, Dictionary<int, DayResult>?> yearResults = new();

    foreach (var puzzle in PuzzleLocator.GetPuzzles(selectedYear, selectedDay))
    {
        string input = ResourceString(puzzle);
        var result = yearResults.GetOrAdd(puzzle.Year, Results)!
            .TryGetValue(puzzle.Day, out var r) ? r : new DayResult();

        AnsiConsole.MarkupLine($"[bold]{puzzle.Year:0000} Day {puzzle.Day:00}[/]: [link=https://adventofcode.com/{puzzle.Year}/day/{puzzle.Day}][dim]{puzzle.Name}[/][/]");
        AnsiConsole.MarkupLine(Run(puzzle, "Part 1", input, (p, s) => p.Part1(s), result.Part1, iterations));
        AnsiConsole.MarkupLine(Run(puzzle, "Part 2", input, (p, s) => p.Part2(s), result.Part2, iterations));
    }
}

static string Run(PuzzleContainer puzzle, string part, string input, Func<IPuzzle, string, object> func, string? expectedResult, int iterations)
{
    var times = new List<TimeSpan>();
    object obj = string.Empty;

    try
    {
        for (int i = 0; i < iterations; i++)
        {
            var start = Stopwatch.GetTimestamp();
            obj = func(puzzle.Puzzle, input);
            var end = Stopwatch.GetTimestamp();
            times.Add(Stopwatch.GetElapsedTime(start, end));
        }
    }
    catch (NotImplementedException)
    {
        return $"\t[bold]{part}[/]: [purple]Not Implemented[/]";
    }

    var elapsed = TimeSpan.FromMicroseconds(times.Average(timeSpan => timeSpan.TotalMicroseconds));
    string timeString = FormatTimeSpan(elapsed);

    (string resultColour, string result) = obj switch
    {
        string s when string.IsNullOrEmpty(s) => ("red", "Incomplete"),
        _ => ("cyan", obj.ToString() ?? string.Empty)
    };

    string checkOrCross = expectedResult switch
    {
        not null when expectedResult == result => "[green]:check_mark:[/]",
        not null when result == "Incomplete" => "",
        not null => "[red]:multiply:[/]",
        _ => "[purple]:exclamation_question_mark:[/]"
    };

    return $"\t[bold]{part}[/]: [[{timeString}]] [{resultColour}]{result}[/] {checkOrCross}";
}

static string ResourceString(PuzzleContainer puzzle)
{
    var assembly = Assembly.GetExecutingAssembly();
    using Stream? manifestResourceStream = assembly.GetManifestResourceStream($"AdventOfCode.Input.Year{puzzle.Year:0000}.Day{puzzle.Day:00}.txt");

    if (manifestResourceStream == null)
    {
        return string.Empty;
    }

    using StreamReader reader = new(manifestResourceStream);
    return reader.ReadToEnd();
}

static Dictionary<int, DayResult> Results(int year)
{
    var assembly = Assembly.GetExecutingAssembly();
    using Stream manifestResourceStream = assembly.GetManifestResourceStream($"AdventOfCode.Input.Year{year:0000}.results.json")!;
    Dictionary<int, DayResult>? results = Deserialize<Dictionary<int, DayResult>>(manifestResourceStream);
    return results ?? new Dictionary<int, DayResult>();
}

static string FormatTimeSpan(TimeSpan elapsed)
{
    string timeColour = elapsed.TotalMilliseconds switch
    {
        > 1000 => "red",
        > 500 => "yellow",
        _ => "green"
    };

    string timeDisplay = elapsed switch
    {
        { TotalSeconds: >= 1 } => $"{elapsed.TotalSeconds:#,##0.00}s ",
        { TotalMilliseconds: >= 1 } => $"{elapsed.TotalMilliseconds:#,##0.00}ms",
        _ => $"{elapsed.TotalMicroseconds:#,##0.00}μs"
    };

    var timeString = $"[{timeColour}]{timeDisplay,12}[/]";
    return timeString;
}
