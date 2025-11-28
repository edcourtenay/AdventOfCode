using Cocona;

namespace AdventOfCode;

public record ApplicationParameters : ICommandParameterSet
{
    [Option]
    [HasDefaultValue]
    public required int Year { get; init; } = DateTime.Today switch
    {
        { Month: >= 1 and <= 11, Year: var y } => y - 1,
        { Year: var y } => y
    };

    [Option]
    [HasDefaultValue]
    public int? Day { get; init; }

    [Option(Description = "Number of times to run each puzzle")]
    [HasDefaultValue]
    public int Iterations { get; init; } = 1;
}