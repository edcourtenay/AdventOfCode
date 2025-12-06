namespace AdventOfCode.Solutions.Year2025;

[Description("Trash Compactor")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        var lines = input.ToLines().ToArray();

        var columns = lines[..^1]
            .Select(s => s.Split(' ', StringSplitOptions.RemoveEmptyEntries))
            .Select(ints => ints.Select(long.Parse))
            .Pivot();

        var operators = lines[^1].Split(' ', StringSplitOptions.RemoveEmptyEntries);

        long sum = 0;
        foreach ((int i, IEnumerable<long> numbers) in columns.Index())
        {
            sum += operators[i] switch
            {
                "+" => numbers.Sum(),
                "*" => numbers.Aggregate(1L, (a, b) => a * b),
                _ => throw new InvalidOperationException($"Unknown operator: {operators[i]}")
            };
        }

        return sum;
    }

    public object Part2(string input)
    {
        long result = 0;
        Stack<long> stack = new();
        foreach (IEnumerable<char> chars in input.ToLines().Pivot().Reverse())
        {
            char[] array = chars.ToArray();

            if (array.All(c => c == ' '))
            {
                stack.Clear();
                continue;
            }

            stack.Push(long.Parse(array.AsSpan()[..^1]));

            switch (array[^1])
            {
                case '+':
                    result += stack.Aggregate(0L, (a, b) => a + b);
                    break;

                case '*':
                    result += stack.Aggregate(1L, (a, b) => a * b);
                    break;
            }
        }

        return result;
    }
}