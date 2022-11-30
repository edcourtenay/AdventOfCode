using System.Collections.Immutable;

namespace AdventOfCode.Year2016;

[Description("Security Through Obscurity")]
public class Day04 : IPuzzle
{
    public object Part1(string input)
    {
        IEnumerable<IGrouping<char,char>> groupBy = "this is a test string".Where(c => c != ' ').GroupBy(c => c);
        ImmutableSortedDictionary<char,int> immutableSortedDictionary = groupBy.ToImmutableSortedDictionary(chars => chars.Key, chars => chars.Count());

        immutableSortedDictionary.
        
        return string.Empty;
    }

    public object Part2(string input)
    {
        return string.Empty;
    }
}