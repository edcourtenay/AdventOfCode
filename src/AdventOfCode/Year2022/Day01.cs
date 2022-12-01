namespace AdventOfCode.Year2022;

[Description("Calorie Counting")]
public class Day01 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, 1);
    }

    public object Part2(string input)
    {
        return Solve(input, 3);
    }

    private static int Solve(string input, int topCount)
    {
        return ToSequences(input.ToLines(), string.IsNullOrEmpty)
            .Select(set => set.Select(int.Parse).Sum())
            .OrderDescending().Take(topCount).Sum();
    }
    
    private static IEnumerable<IEnumerable<string>> ToSequences(IEnumerable<string> source, Func<string, bool> predicate)
    {
        IEnumerable<string> GroupSequence(IEnumerator<string> enumerator, Func<string, bool> func)
        {
            do
            {
                yield return enumerator.Current;
            } while (enumerator.MoveNext() && !func(enumerator.Current));
        }

        IEnumerable<IEnumerable<string>> ToSequencesInternal(IEnumerable<string> enumerable, Func<string,bool> func)
        {
            IEnumerator<string> enumerator = enumerable.GetEnumerator();

            do
            {
                if (!enumerator.MoveNext())
                    yield break;

                yield return GroupSequence(enumerator, func);
            } while (true);
        }

        return ToSequencesInternal(source, predicate);
    }
}