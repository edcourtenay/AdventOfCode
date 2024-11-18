namespace AdventOfCode.Solutions.Year2019;

[Description("The Tyranny of the Rocket Equation")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return Enumerable
            .SelectMany<int, int>(input.ToLines(int.Parse), i => FuelWeights(i, false))
            .Sum();
    }

    public object Part2(string input)
    {
        return Enumerable
            .SelectMany<int, int>(input.ToLines(int.Parse), i => FuelWeights(i, true))
            .Sum();
    }

    private static IEnumerable<int> FuelWeights(int mass, bool recurse)
    {
        while ((mass = (mass / 3) - 2) > 0)
        {
            yield return mass;

            if (!recurse)
            {
                yield break;
            }
        }
    }
}