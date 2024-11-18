using System.Reflection;
using System.Text.RegularExpressions;

using AdventOfCode.Solutions;

namespace AdventOfCode;

public partial record PuzzleContainer
{
    public required IPuzzle Puzzle { get; init; }
    public required int Year { get; init; }
    public required int Day { get; init; }
    public required string Name { get; init; }

    public static PuzzleContainer? FromType(Type type)
    {
        if (Activator.CreateInstance(type) is not IPuzzle puzzle)
        {
            return null;
        }

        var match = YearDayRegex().Match(type.FullName!);
        if (!match.Success)
        {
            return null;
        }

        string description = type.GetCustomAttribute<DescriptionAttribute>() is { } descriptionAttribute
            ? descriptionAttribute.Description
            : "Unknown";

        return new PuzzleContainer
        {
            Puzzle = puzzle,
            Name = description,
            Year = int.Parse(match.Groups["year"].Value),
            Day = int.Parse(match.Groups["day"].Value)
        };
    }

    [GeneratedRegex(@"Year(?<year>\d+)\.Day(?<day>\d+)")]
    private static partial Regex YearDayRegex();
}