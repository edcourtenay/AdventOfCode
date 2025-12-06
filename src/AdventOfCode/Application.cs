using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Text.Json;
using AdventOfCode.Solutions;
using Cocona;
using Spectre.Console;

namespace AdventOfCode;

public class Application
{
    private readonly InputDownloader _downloader;

    public Application(InputDownloader downloader) => _downloader = downloader;

    [PrimaryCommand]
    public async Task ExecutePuzzles(ApplicationParameters parameters, [Ignore] CancellationToken ct = default)
    {
        ConcurrentDictionary<int, Dictionary<int, DayResult>> yearResults = new();

        foreach (var puzzle in PuzzleRegistry.GetPuzzles(parameters.Year, parameters.Day))
        {
            var input = await _downloader.ReadOrDownload(puzzle.Year, puzzle.Day, ct);
            var expected = GetExpectedResult(yearResults, puzzle);

            PrintHeader(puzzle);
            AnsiConsole.MarkupLine(RunPart(puzzle, "Part 1", input, p => p.Part1(input), expected.Part1, parameters));
            AnsiConsole.MarkupLine(RunPart(puzzle, "Part 2", input, p => p.Part2(input), expected.Part2, parameters));
        }
    }

    string RunPart(PuzzleContainer puzzle, string part, string input, Func<IPuzzle, object> func, string? expectedResult, ApplicationParameters parameters)
    {
        try
        {
            var (times, result) = MeasureExecutionTimes(puzzle.Puzzle, func, parameters.Iterations);
            var elapsed = TimeSpan.FromMicroseconds(times.Average(t => t.TotalMicroseconds));
            var (color, resultText) = FormatResult(result);
            var status = GetStatusIcon(expectedResult, resultText);
            var display = parameters.HideResults ? "" : resultText;

            return $"\t[bold]{part}[/]: [[{FormatTime(elapsed)}]] [{color}]{display}[/] {status}";
        }
        catch (NotImplementedException)
        {
            return $"\t[bold]{part}[/]: [purple]Not Implemented[/]";
        }
    }

    (List<TimeSpan> Times, object Result) MeasureExecutionTimes(IPuzzle puzzle, Func<IPuzzle, object> func, int iterations)
    {
        var times = new List<TimeSpan>(iterations);
        object result = string.Empty;

        for (var i = 0; i < iterations; i++)
        {
            var start = Stopwatch.GetTimestamp();
            result = func(puzzle);
            times.Add(Stopwatch.GetElapsedTime(start));
        }

        return (times, result);
    }

    (string Color, string Text) FormatResult(object result) =>
        result switch
        {
            string s when string.IsNullOrEmpty(s) => ("red", "Incomplete"),
            _ => ("cyan", result.ToString() ?? string.Empty)
        };

    string GetStatusIcon(string? expected, string actual) =>
        expected switch
        {
            null => "[purple]‽[/]",
            _ when actual == "Incomplete" => "",
            _ when expected == actual => "[green]✓[/]",
            _ => "[red]✕[/]"
        };

    void PrintHeader(PuzzleContainer puzzle) =>
        AnsiConsole.MarkupLine($"[bold]{puzzle.Year:0000} Day {puzzle.Day:00}[/]: [link=https://adventofcode.com/{puzzle.Year}/day/{puzzle.Day}][dim]{puzzle.Name}[/][/]");

    DayResult GetExpectedResult(ConcurrentDictionary<int, Dictionary<int, DayResult>> cache, PuzzleContainer puzzle)
    {
        var yearResults = cache.GetOrAdd(puzzle.Year, LoadResults);
        return yearResults.TryGetValue(puzzle.Day, out var result) ? result : new DayResult();
    }

    Dictionary<int, DayResult> LoadResults(int year)
    {
        var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"AdventOfCode.Input.Year{year:0000}.results.json")!;
        return JsonSerializer.Deserialize<Dictionary<int, DayResult>>(stream) ?? new Dictionary<int, DayResult>();
    }

    string FormatTime(TimeSpan elapsed) => $"[{GetTimeColor(elapsed)}]{GetTimeDisplay(elapsed),12}[/]";

    string GetTimeColor(TimeSpan elapsed) =>
        elapsed.TotalMilliseconds switch
        {
            > 1000 => "red",
            > 500 => "yellow",
            _ => "green"
        };

    string GetTimeDisplay(TimeSpan elapsed) =>
        elapsed switch
        {
            { TotalSeconds: >= 1 } => $"{elapsed.TotalSeconds:#,##0.00}s ",
            { TotalMilliseconds: >= 1 } => $"{elapsed.TotalMilliseconds:#,##0.00}ms",
            _ => $"{elapsed.TotalMicroseconds:N0}μs"
        };
}