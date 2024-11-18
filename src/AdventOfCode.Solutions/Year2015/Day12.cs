using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace AdventOfCode.Solutions.Year2015;

[Description("JSAbacusFramework.io")]
[SuppressMessage("ReSharper", "UnusedType.Global")]
public class Day12 : IPuzzle
{
    public object Part1(string input) => CountValues(JsonDocument.Parse(input), false);

    public object Part2(string input) => CountValues(JsonDocument.Parse(input), true);

    private static int CountValues(JsonDocument document, bool skipRed)
    {
        bool SkipRed(JsonElement element)
        {
            return skipRed && element.EnumerateObject().Any(property =>
                property.Value is { ValueKind: JsonValueKind.String } && property.Value.GetString() == "red");
        }

        int WalkDocument(JsonElement element)
        {
            return element.ValueKind switch
            {
                JsonValueKind.Object when SkipRed(element) => 0,
                JsonValueKind.Object => element.EnumerateObject().Select(property => WalkDocument(property.Value))
                    .Sum(),
                JsonValueKind.Array => element.EnumerateArray().Select(WalkDocument).Sum(),
                JsonValueKind.Number => element.GetInt32(),
                _ => 0
            };
        }

        return WalkDocument(document.RootElement);
    }
}