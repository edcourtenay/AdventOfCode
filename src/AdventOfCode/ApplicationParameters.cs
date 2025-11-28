using Cocona;

using JetBrains.Annotations;

namespace AdventOfCode;

[UsedImplicitly]
public record ApplicationParameters : ICommandParameterSet
{
    [Option]
    [HasDefaultValue]
    public required int Year { get; init; } = DateTime.Today switch
    {
        { Month: >= 1 and <= 11, Year: var currentYear } => currentYear - 1,
        { Year: var currentYear } => currentYear
    };

    [Option]
    [HasDefaultValue]
    public int? Day { get; init; }

    [Option(Description = "Number of times to run each puzzle")]
    [HasDefaultValue]
    public int Iterations { get; init; } = 1;
}