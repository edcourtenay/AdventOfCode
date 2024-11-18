using System.Text.Json.Serialization;

namespace AdventOfCode;

public record DayResult
{
    [JsonPropertyName("part1")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Part1 { get; init; } = null; 
    
    [JsonPropertyName("part2")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Part2 { get; init; } = null;
}