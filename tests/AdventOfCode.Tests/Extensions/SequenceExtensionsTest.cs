using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Tests.Extensions;

public class SequenceExtensionsTest
{
    [Fact(DisplayName = "Flatten should flatten tree structure")]
    public void FlattenTreeTest()
    {
        var root = new Node(1,
            new Node(2,
                new Node(4),
                new Node(5)),
            new Node(3));

        var flattened = new[] { root }.Flatten(n => n.Children).ToList();
        
        // Post-order: left children, right children, then node
        flattened.Select(n => n.Value).Should().Equal(4, 5, 2, 3, 1);
    }

    [Fact(DisplayName = "Flatten should handle nodes with no children")]
    public void FlattenLeafNodesTest()
    {
        var nodes = new[]
        {
            new Node(1),
            new Node(2),
            new Node(3)
        };

        var flattened = nodes.Flatten(n => n.Children).ToList();
        
        flattened.Select(n => n.Value).Should().Equal(1, 2, 3);
    }

    [Fact(DisplayName = "Flatten should handle empty sequence")]
    public void FlattenEmptySequenceTest()
    {
        var nodes = Array.Empty<Node>();
        var flattened = nodes.Flatten(n => n.Children).ToList();
        
        flattened.Should().BeEmpty();
    }

    [Fact(DisplayName = "Flatten should handle deep nesting")]
    public void FlattenDeepNestingTest()
    {
        var root = new Node(1,
            new Node(2,
                new Node(3,
                    new Node(4))));

        var flattened = new[] { root }.Flatten(n => n.Children).ToList();
        
        flattened.Select(n => n.Value).Should().Equal(4, 3, 2, 1);
    }

    [Fact(DisplayName = "Flatten should throw if children selector is null")]
    public void FlattenNullSelectorTest()
    {
        var nodes = new[] { new Node(1) };
        var act = () => nodes.Flatten(null!).ToList();
        
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("children");
    }

    [Fact(DisplayName = "TakeUntil should take items until predicate is true")]
    public void TakeUntilTest()
    {
        int[] numbers = [1, 2, 3, 4, 5, 6];
        var result = numbers.TakeUntil(x => x == 4).ToList();
        
        result.Should().Equal(1, 2, 3, 4);
    }

    [Fact(DisplayName = "TakeUntil should take all if predicate never matches")]
    public void TakeUntilNoMatchTest()
    {
        int[] numbers = [1, 2, 3, 4];
        var result = numbers.TakeUntil(x => x == 10).ToList();
        
        result.Should().Equal(1, 2, 3, 4);
    }

    [Fact(DisplayName = "TakeUntil should handle empty sequence")]
    public void TakeUntilEmptySequenceTest()
    {
        int[] numbers = [];
        var result = numbers.TakeUntil(x => x == 1).ToList();
        
        result.Should().BeEmpty();
    }

    [Fact(DisplayName = "TakeUntil should throw if predicate is null")]
    public void TakeUntilNullPredicateTest()
    {
        int[] numbers = [1, 2, 3];
        Action act = () => numbers.TakeUntil(null!).ToList();
        
        act.Should().Throw<ArgumentNullException>()
            .WithParameterName("predicate");
    }

    [Fact(DisplayName = "Pivot should transpose sequences")]
    public void PivotTest()
    {
        var sequences = new[]
        {
            new[] { 1, 2, 3 },
            new[] { 4, 5, 6 },
            new[] { 7, 8, 9 }
        };

        var pivoted = sequences.Pivot().ToList();
        
        pivoted.Should().HaveCount(3);
        pivoted[0].Should().Equal(1, 4, 7);
        pivoted[1].Should().Equal(2, 5, 8);
        pivoted[2].Should().Equal(3, 6, 9);
    }

    [Fact(DisplayName = "Pivot should handle empty input")]
    public void PivotEmptyInputTest()
    {
        var sequences = Array.Empty<int[]>();
        var pivoted = sequences.Pivot().ToList();
        
        pivoted.Should().BeEmpty();
    }

    [Fact(DisplayName = "Pivot should stop at shortest sequence")]
    public void PivotUnequalLengthsTest()
    {
        var sequences = new[]
        {
            new[] { 1, 2, 3, 4 },
            new[] { 5, 6 },
            new[] { 7, 8, 9 }
        };

        var pivoted = sequences.Pivot().ToList();
        
        pivoted.Should().HaveCount(2);
        pivoted[0].Should().Equal(1, 5, 7);
        pivoted[1].Should().Equal(2, 6, 8);
    }

    private class Node
    {
        public int Value { get; }
        public Node[] Children { get; }

        public Node(int value, params Node[] children)
        {
            Value = value;
            Children = children;
        }
    }
}

