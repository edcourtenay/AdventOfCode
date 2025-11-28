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

    public Application(InputDownloader downloader)
    {
        _downloader = downloader;
    }

    [PrimaryCommand]
    public async Task ExecutePuzzles(ApplicationParameters applicationParameters, [Ignore] CancellationToken ct = default)
    {
        ConcurrentDictionary<int, Dictionary<int, DayResult>?> yearResults = new();

        foreach (var puzzle in PuzzleLocator.GetPuzzles(applicationParameters.Year, applicationParameters.Day))
        {
            string input = await GetInputStringAsync(puzzle, ct);
            var result = yearResults.GetOrAdd(puzzle.Year, Results)!
                .TryGetValue(puzzle.Day, out var r)
                ? r
                : new DayResult();

            AnsiConsole.MarkupLine(
                $"[bold]{puzzle.Year:0000} Day {puzzle.Day:00}[/]: [link=https://adventofcode.com/{puzzle.Year}/day/{puzzle.Day}][dim]{puzzle.Name}[/][/]");
            AnsiConsole.MarkupLine(Run(puzzle, "Part 1", input, (p, s) => p.Part1(s), result.Part1, applicationParameters.Iterations));
            AnsiConsole.MarkupLine(Run(puzzle, "Part 2", input, (p, s) => p.Part2(s), result.Part2, applicationParameters.Iterations));
        }
    }

    string Run(PuzzleContainer puzzle, string part, string input, Func<IPuzzle, string, object> func,
        string? expectedResult, int iterations)
    {
        var times = new List<TimeSpan>();
        object obj = string.Empty;

        try
        {
            for (int i = 0; i < iterations; i++)
            {
                var start = Stopwatch.GetTimestamp();
                obj = func(puzzle.Puzzle, input);
                times.Add(Stopwatch.GetElapsedTime(start));
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

    async Task<string> GetInputStringAsync(PuzzleContainer puzzle, CancellationToken ct)
    {
        return await _downloader.ReadOrDownload(puzzle.Year, puzzle.Day, ct);
    }

    Dictionary<int, DayResult> Results(int year)
    {
        var assembly = Assembly.GetExecutingAssembly();
        using Stream manifestResourceStream =
            assembly.GetManifestResourceStream($"AdventOfCode.Input.Year{year:0000}.results.json")!;
        Dictionary<int, DayResult>? results = JsonSerializer.Deserialize<Dictionary<int, DayResult>>(manifestResourceStream);
        return results ?? new Dictionary<int, DayResult>();
    }

    string FormatTimeSpan(TimeSpan elapsed) =>
        $"[{TimeColour(elapsed)}]{TimeDisplay(elapsed),12}[/]";

    string TimeColour(TimeSpan elapsed) =>
        elapsed.TotalMilliseconds switch
        {
            > 1000 => "red",
            > 500 => "yellow",
            _ => "green"
        };

    string TimeDisplay(TimeSpan elapsed) =>
        elapsed switch
        {
            { TotalSeconds: >= 1 } => $"{elapsed.TotalSeconds:#,##0.00}s ",
            { TotalMilliseconds: >= 1 } => $"{elapsed.TotalMilliseconds:#,##0.00}ms",
            _ => $"{elapsed.TotalMicroseconds:N0}μs"
        };
}