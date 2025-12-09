using AdventOfCode.Solutions.Extensions;

using Range = (int from, int to);

namespace AdventOfCode.Tests.Extensions;

public class RangeExtensionsTest
{
    [Fact(DisplayName = "Merge should return expected results from example data")]
    public void MergeTests()
    {
        Range[] ranges = [(1, 3), (2, 4), (5, 7), (6, 8), (9, 11), (10, 12)];
        Range[] merged = ranges.Merge().ToArray();
        merged.Should().BeEquivalentTo([(1, 12)]);
    }

    [Fact(DisplayName = "ExcludeRange should return expected results from example data")]
    public void ExcludeRangeTests()
    {
        Range source = (1, 4000);
        Range exclude = (1000, 2000);
        IEnumerable<Range> excluded = source.ExcludeRange(exclude);
        excluded.Should().BeEquivalentTo([(1, 999), (2001, 4000)]);
    }

    [Fact(DisplayName = "ExcludeRanges should return expected results from example data")]
    public void ExcludeRangesTests()
    {
        Range[] source = [(1, 3), (5, 7), (9, 11)];
        Range[] exclude = [(2, 4), (6, 8), (10, 12)];
        Range[] excluded = source.ExcludeRanges(exclude).ToArray();
        excluded.Should().BeEquivalentTo([(1, 1), (5, 5), (9, 9)]);
    }

    [Fact(DisplayName = "Contains should return true when value is in range")]
    public void ContainsInRangeTest()
    {
        Range range = (1, 10);
        range.Contains(5).Should().BeTrue();
        range.Contains(1).Should().BeTrue();
        range.Contains(10).Should().BeTrue();
    }

    [Fact(DisplayName = "Contains should return false when value is outside range")]
    public void ContainsOutsideRangeTest()
    {
        Range range = (1, 10);
        range.Contains(0).Should().BeFalse();
        range.Contains(11).Should().BeFalse();
    }

    [Fact(DisplayName = "Intersects should return true for overlapping ranges")]
    public void IntersectsOverlappingTest()
    {
        Range range1 = (1, 10);
        Range range2 = (5, 15);
        range1.Intersects(range2).Should().BeTrue();
        range2.Intersects(range1).Should().BeTrue();
    }

    [Fact(DisplayName = "Intersects should return true for touching ranges")]
    public void IntersectsTouchingTest()
    {
        Range range1 = (1, 10);
        Range range2 = (10, 20);
        range1.Intersects(range2).Should().BeTrue();
        range2.Intersects(range1).Should().BeTrue();
    }

    [Fact(DisplayName = "Intersects should return false for non-overlapping ranges")]
    public void IntersectsNonOverlappingTest()
    {
        Range range1 = (1, 10);
        Range range2 = (15, 20);
        range1.Intersects(range2).Should().BeFalse();
        range2.Intersects(range1).Should().BeFalse();
    }

    [Fact(DisplayName = "Intersects should return true when one range contains another")]
    public void IntersectsContainedTest()
    {
        Range range1 = (1, 100);
        Range range2 = (10, 20);
        range1.Intersects(range2).Should().BeTrue();
        range2.Intersects(range1).Should().BeTrue();
    }

    [Fact(DisplayName = "RangeLength should return correct length")]
    public void RangeLengthTest()
    {
        Range range1 = (1, 10);
        range1.RangeLength().Should().Be(10);

        Range range2 = (5, 5);
        range2.RangeLength().Should().Be(1);

        Range range3 = (0, 99);
        range3.RangeLength().Should().Be(100);
    }

    [Fact(DisplayName = "ExcludeRange single should handle no intersection")]
    public void ExcludeRangeSingleNoIntersectionTest()
    {
        Range source = (1, 10);
        Range exclude = (20, 30);
        var result = source.ExcludeRange(exclude).ToArray();
        result.Should().BeEquivalentTo([(1, 10)]);
    }

    [Fact(DisplayName = "ExcludeRange single should handle full coverage")]
    public void ExcludeRangeSingleFullCoverageTest()
    {
        Range source = (5, 10);
        Range exclude = (1, 20);
        var result = source.ExcludeRange(exclude).ToArray();
        result.Should().BeEmpty();
    }

    [Fact(DisplayName = "ExcludeRange single should split range")]
    public void ExcludeRangeSingleSplitTest()
    {
        Range source = (1, 10);
        Range exclude = (4, 6);
        var result = source.ExcludeRange(exclude).ToArray();
        result.Should().BeEquivalentTo([(1, 3), (7, 10)]);
    }

    [Fact(DisplayName = "ExcludeRange single should handle left overlap")]
    public void ExcludeRangeSingleLeftOverlapTest()
    {
        Range source = (5, 15);
        Range exclude = (1, 10);
        var result = source.ExcludeRange(exclude).ToArray();
        result.Should().BeEquivalentTo([(11, 15)]);
    }

    [Fact(DisplayName = "ExcludeRange single should handle right overlap")]
    public void ExcludeRangeSingleRightOverlapTest()
    {
        Range source = (5, 15);
        Range exclude = (10, 20);
        var result = source.ExcludeRange(exclude).ToArray();
        result.Should().BeEquivalentTo([(5, 9)]);
    }

    [Fact(DisplayName = "ExcludeRanges on enumerable should handle empty source")]
    public void ExcludeRangesEmptySourceTest()
    {
        Range[] source = [];
        Range[] exclude = [(1, 10)];
        var result = source.ExcludeRanges(exclude).ToArray();
        result.Should().BeEmpty();
    }

    [Fact(DisplayName = "ExcludeRanges on enumerable should handle empty exclude")]
    public void ExcludeRangesEmptyExcludeTest()
    {
        Range[] source = [(1, 10), (20, 30)];
        Range[] exclude = [];
        var result = source.ExcludeRanges(exclude).ToArray();
        result.Should().BeEquivalentTo([(1, 10), (20, 30)]);
    }
}