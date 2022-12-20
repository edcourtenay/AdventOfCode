namespace AdventOfCode.Year2022;

[Description("Grove Positioning System")]
public class Day20 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, 1, (x, i) => (long.Parse(x), i));
    }

    public object Part2(string input)
    {
        return Solve(input, 10, (x, i) => (long.Parse(x) * 811589153, i));
    }

    private static long Solve(string input, int iterations, Func<string, int, (long value, int originalIndex)> selector)
    {
        List<(long val, int salt)> numbers = input
            .ToLines(selector)
            .ToList();

        List<(long value, int originalIndex)> mixedNumbers = new(numbers);

        for (int i = 0; i < iterations; i++)
        {
            Mix(numbers, mixedNumbers);
        }

        int zeroIndex = mixedNumbers.IndexOf(numbers.Find(x => x.val == 0));

        return new[] { 1000, 2000, 3000 }
            .Sum(i => mixedNumbers[(i + zeroIndex) % mixedNumbers.Count].value);
    }

    private static void Mix(List<(long value, int originalIndex)> startNumbers,
        List<(long value, int originalIndex)> result)
    {
        foreach (var number in startNumbers) {
            int oldIndex = result.IndexOf(number);
            int newIndex = (int)((oldIndex + number.value) % (result.Count - 1));
            if (newIndex <= 0 && oldIndex + number.value != 0)
            {
                newIndex = result.Count - 1 + newIndex;
            }

            result.RemoveAt(oldIndex);
            result.Insert(newIndex, number);
        }
    }
}