namespace AdventOfCode.Year2022;

[Description("Calorie Counting")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return SplitByBlankLine(input).Max();
    }

    public object Part2(string input)
    {
        return SplitByBlankLine(input).OrderDescending().Take(3).Sum();
    }

    private static IEnumerable<int> SplitByBlankLine(string input)
    {
        return ToGroups(input.ToLines(), s => !string.IsNullOrEmpty(s))
            .Select(set => set.Select(int.Parse).Sum());
    }
    
    private static IEnumerable<IEnumerable<string>> ToGroups(IEnumerable<string> source, Func<string, bool> predicate)
    {
        IEnumerable<string> GroupSequence(IEnumerator<string> enumerator, Func<string, bool> func)
        {
            do
            {
                yield return enumerator.Current;
            } while (enumerator.MoveNext() && func(enumerator.Current));
        }

        IEnumerable<IEnumerable<string>> ToGroupsInternal(IEnumerable<string> enumerable, Func<string,bool> func)
        {
            IEnumerator<string> enumerator = enumerable.GetEnumerator();

            do
            {
                if (!enumerator.MoveNext())
                    yield break;

                yield return GroupSequence(enumerator, func);
            } while (true);
        }

        return ToGroupsInternal(source, predicate);
    }
}