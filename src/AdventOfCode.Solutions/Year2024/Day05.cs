namespace AdventOfCode.Solutions.Year2024;

[Description("Print Queue")]
public class Day05 : IPuzzle
{
    public object Part1(string input)
    {
        var (comparer, pages) = Parse(input.ToLines());

        return pages
            .Where(x => SortedCorrectly(x, comparer))
            .Sum(x => int.Parse(x[x.Length / 2]));
    }

    public object Part2(string input)
    {
        var (comparer, pages) = Parse(input.ToLines());
        
        return pages
            .Where(x => !SortedCorrectly(x, comparer))
            .Sum(x => int.Parse(x.Order(comparer).ElementAt(x.Length / 2)));
    }
    
    static (PageComparer comparer, List<string[]> pages) Parse(IEnumerable<string> lines)
    {
        HashSet<(string, string)> rules = [];
        List<string[]> pages = [];
        PageComparer comparer = new(rules);

        using var enumerator = lines.GetEnumerator();
        var blankSeen = false;
        while (enumerator.MoveNext())
        {
            var line = enumerator.Current;

            switch (line)
            {
                case "":
                    blankSeen = true;
                    break;
                
                case not null when !blankSeen:
                    var split = line.Split('|');
                    if (split is [{ } a, { } b])
                    {
                        rules.Add((a, b));
                    }

                    break;
                    
                case not null when blankSeen:
                    pages.Add(line.Split(','));
                    break;
            }
        }

        return (comparer, pages);
    }
    
    static bool SortedCorrectly(string[] page, PageComparer comparer)
        => page.SequenceEqual(page.Order(comparer));
    
    class PageComparer(HashSet<(string, string)> rules) : IComparer<string>
    {
        public int Compare(string? x, string? y)
        {
            ArgumentNullException.ThrowIfNull(x);
            ArgumentNullException.ThrowIfNull(y);

            if (rules.Contains((x, y)))
            {
                return -1;
            }

            return rules.Contains((y, x)) ? 1 : 0;
        }
    }
}
