using System.Reflection;
using System.Text.RegularExpressions;

using AdventOfCode2015CS;

var dayRegex = new Regex(@"(?<name>[A-Za-z]+)(?<index>\d+)");

var types = typeof(Program).Assembly
    .GetTypes();

var puzzles = types
    .Where(t => typeof(IPuzzleBase).IsAssignableFrom(t))
    .Where(t => !t.IsInterface)
    .Where(t => !t.IsAbstract)
    .OrderBy(t => t.Name);

foreach (var puzzleType in puzzles)
{
    if (Activator.CreateInstance(puzzleType) is not IPuzzleBase puzzle)
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
        ? $@"{match.Groups["name"].Value} {match.Groups["index"].Value}"
        : puzzleTypeName;
}

string ResourceString(MemberInfo puzzleType)
{
    var assembly = typeof(Program).GetTypeInfo().Assembly;
    using Stream manifestResourceStream = assembly.GetManifestResourceStream($"AdventOfCode2022.Input.{puzzleType.Name}.txt")!;
    using StreamReader reader = new(manifestResourceStream);

    return reader.ReadToEnd();
}