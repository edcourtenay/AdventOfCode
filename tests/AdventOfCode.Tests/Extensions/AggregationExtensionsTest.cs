using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Tests.Extensions;

public class AggregationExtensionsTest
{
    [Fact(DisplayName = "MinMax should return min and max for comparable types")]
    public void MinMaxTest()
    {
        int[] numbers = [5, 2, 8, 1, 9, 3];
        var (min, max) = numbers.MinMax();
        min.Should().Be(1);
        max.Should().Be(9);
    }

    [Fact(DisplayName = "MinMax should work with single element")]
    public void MinMaxSingleElementTest()
    {
        int[] numbers = [42];
        var (min, max) = numbers.MinMax();
        min.Should().Be(42);
        max.Should().Be(42);
    }

    [Fact(DisplayName = "MinMax should throw for empty sequence")]
    public void MinMaxEmptySequenceTest()
    {
        int[] numbers = [];
        var act = () => numbers.MinMax();
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Sequence contains no elements.");
    }

    [Fact(DisplayName = "MinMax with comparer should work")]
    public void MinMaxWithComparerTest()
    {
        string[] words = ["apple", "zoo", "cat", "a"];
        var comparer = Comparer<string>.Create((a, b) => a.Length.CompareTo(b.Length));
        var (min, max) = words.MinMax(comparer);
        min.Should().Be("a");
        max.Should().Be("apple");
    }

    [Fact(DisplayName = "MinMaxBy should return min and max based on selector")]
    public void MinMaxByTest()
    {
        var people = new[]
        {
            new { Name = "Alice", Age = 30 },
            new { Name = "Bob", Age = 25 },
            new { Name = "Charlie", Age = 35 }
        };
        var (min, max) = people.MinMaxBy(p => p.Age);
        min.Should().Be(25);
        max.Should().Be(35);
    }

    [Fact(DisplayName = "MinMaxBy should work with strings")]
    public void MinMaxByStringTest()
    {
        string[] words = ["apple", "zoo", "cat", "elephant"];
        var (min, max) = words.MinMaxBy(w => w.Length);
        min.Should().Be(3);
        max.Should().Be(8);
    }

    [Fact(DisplayName = "MinMaxBy should throw for empty sequence")]
    public void MinMaxByEmptySequenceTest()
    {
        var items = Array.Empty<string>();
        var act = () => items.MinMaxBy(s => s.Length);
        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Sequence contains no elements.");
    }
}

