using Range = (int from, int to);

namespace AdventOfCode.Tests;

public class RangeExtensionsTest
{
    [Fact(DisplayName = "MergeOverlapping should return expected results from example data")]
    public void MergeOverlappingTests()
    {
        Range[] ranges = { (1, 3), (2, 4), (5, 7), (6, 8), (9, 11), (10, 12) };
        var merged = ranges.MergeOverlapping().ToArray();
        merged.Should().BeEquivalentTo(new[] { (1, 4), (5, 8), (9, 12) });
    }

    [Fact(DisplayName = "ExcludeRange should return expected results from example data")]
    public void ExcludeRangeTests()
    {
        Range source = (1, 4000);
        Range exclude = (1000, 2000);
        var excluded = source.ExcludeRange(exclude);
        excluded.Should().BeEquivalentTo(new[] { (1, 999), (2001, 4000) });
    }

    [Fact(DisplayName = "ExcludeRanges should return expected results from example data")]
    public void ExcludeRangesTests()
    {
        Range[] source = { (1, 3), (5, 7), (9, 11) };
        Range[] exclude = { (2, 4), (6, 8), (10, 12) };
        var excluded = source.ExcludeRanges(exclude).ToArray();
        excluded.Should().BeEquivalentTo(new[] { (1, 1), (5, 5), (9, 9) });
    }
}