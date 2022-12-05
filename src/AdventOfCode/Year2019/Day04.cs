namespace AdventOfCode.Year2019;

[Description("Secure Container")]
public class Day04 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, i => IsValid(i.ToString()));
    }

    public object Part2(string input)
    {
        return Solve(input, i => IsValid2(i.ToString()));
    }

    private static object Solve(string input, Func<int, bool> predicate)
    {
        var range = input.Split('-').Select(int.Parse).ToArray();
        return Enumerable.Range(range[0], range[1] - range[0] + 1)
            .Count(predicate);
    }

    private static bool IsValid(string input)
    {
        bool allAscending = input.Pairwise().All(tuple => tuple.First <= tuple.Second);
        return allAscending && input.Pairwise().Any(tuple => tuple.First == tuple.Second);
    }

    public static bool IsValid2(string input)
    {
        var pairsSeen = new int[10];

        foreach ((char first, char second) in input.Pairwise())
        {
            if (first > second)
            {
                return false;
            }

            if (first == second)
            {
                pairsSeen[first - '0'] += 1;
            }
        }

        return pairsSeen.Any(i => i == 1);
    }
}