using System.Reflection;

using AdventOfCode.Solutions;

namespace AdventOfCode;

public static class PuzzleLocator
{
    public static IEnumerable<PuzzleContainer> GetPuzzles(int year, int? day)
    {
        return Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load)
            .SelectMany(a => a.GetTypes()
                .Where(t => typeof(IPuzzle).IsAssignableFrom(t))
                .Where(t => t is { IsInterface: false, IsAbstract: false })
            )
            .Distinct()
            .Select(PuzzleContainer.FromType)
            .OfType<PuzzleContainer>()
            .OrderBy(t => t.Year)
            .ThenBy(t => t.Day)
            .Where(t => t.Year == year && ((day.HasValue && t.Day == day.Value) || !day.HasValue));
    }
}