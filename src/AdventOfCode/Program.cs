using System.Reflection;
using System.Text.RegularExpressions;

using AdventOfCode;

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
    
    Console.WriteLine($"{name}: {description}");
    Console.WriteLine($"\tPart 01: {puzzle.Part1(input)}");
    Console.WriteLine($"\tPart 02: {puzzle.Part2(input)}");
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