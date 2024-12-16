namespace AdventOfCode.Solutions.Year2024;

[Description("Not Set")]
public class Day07 : IPuzzle
{
    public object Part1(string input)
    {
        var lines = input.ToLines(ParseLine);

        return lines.Where(EvaluateLine).Sum(l => l.target);
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    public (long target, long[] numbers) ParseLine(string line)
    {
        string[] strings = line.Split([':', ' '], StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);

        return (long.Parse(strings[0]), [..strings[1..].Select(long.Parse)]);
    }

    public bool EvaluateLine((long target, long[] numbers) line)
    {
        int combinations = 0x1 << (line.numbers.Length - 1);
        for (int i = 0; i < combinations; i++)
        {
            int x = i;
            long result = line.numbers[0];
            for (int j = 1; j < line.numbers.Length; j++)
            {
                switch (x & 0x1)
                {
                    case 0x0:
                        result += line.numbers[j];
                        break;
                    default:
                        result *= line.numbers[j];
                        break;
                }

                x >>= 1;
            }

            if (result == line.target)
            {
                return true;
            }
        }

        return false;
    }
}
