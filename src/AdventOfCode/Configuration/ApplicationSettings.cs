using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AdventOfCode.Configuration;

public record ApplicationSettings : IValidateOptions<ApplicationSettings>
{
    [ConfigurationKeyName("AOC_SESSION")]
    public required string SessionId { get; init; }

    public ValidateOptionsResult Validate(string? name, ApplicationSettings options)
    {
        if (string.IsNullOrWhiteSpace(options.SessionId))
        {
            return ValidateOptionsResult.Fail("Advent of Code Session ID is required");
        }

        return ValidateOptionsResult.Success;
    }
}