namespace AdventOfCode.Solutions.Year2018;

[Description("Inventory Management System")]
public class Day02 : IPuzzle
{
    public object Part1(string input)
    {
        var results = Enumerable.ToArray(input.ToLines(s => new { Two = ContainsRepeatedChars(s, 2), Three = ContainsRepeatedChars(s, 3) }));

        var count2 = Enumerable.Count(results, arg => arg.Two);
        var count3 = Enumerable.Count(results, arg => arg.Three);

        return count2 * count3;
    }

    public object Part2(string input)
    {
        List<string> boxIds = Enumerable.ToList<string>(input.ToLines());
        for (int i = 0; i < boxIds.Count; i++) {
            for (int j = i + 1; j < boxIds.Count; j++) {
                int diffCount = 0;
                int diffIndex = 0;
                for (int k = 0; k < boxIds[i].Length; k++)
                {
                    if (boxIds[i][k] == boxIds[j][k])
                    {
                        continue;
                    }

                    diffCount++;
                    diffIndex = k;
                    if (diffCount > 1) break;
                }
                if (diffCount == 1) {
                    return boxIds[i].Remove(diffIndex, 1);
                }
            }
        }
        return "";
    }

    public bool ContainsRepeatedChars(string input, int repeat) => input.GroupBy(c => c).Any(g => g.Count() == repeat);
}