using System.Diagnostics.CodeAnalysis;

using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Tests.Extensions;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class ArrayExtensionsTest
{
    [Fact(DisplayName = "Permutations should generate all permutations")]
    public void PermutationsTest()
    {
        int[] numbers = [1, 2, 3];
        var permutations = numbers.Permutations().ToList();
        
        permutations.Should().HaveCount(6); // 3! = 6
        permutations.Should().ContainEquivalentOf(new[] { 1, 2, 3 });
        permutations.Should().ContainEquivalentOf(new[] { 1, 3, 2 });
        permutations.Should().ContainEquivalentOf(new[] { 2, 1, 3 });
        permutations.Should().ContainEquivalentOf(new[] { 2, 3, 1 });
        permutations.Should().ContainEquivalentOf(new[] { 3, 1, 2 });
        permutations.Should().ContainEquivalentOf(new[] { 3, 2, 1 });
    }

    [Fact(DisplayName = "Permutations should handle single element")]
    public void PermutationsSingleElementTest()
    {
        int[] numbers = [42];
        var permutations = numbers.Permutations().ToList();
        
        permutations.Should().HaveCount(1);
        permutations[0].Should().Equal(42);
    }

    [Fact(DisplayName = "Permutations should handle empty array")]
    public void PermutationsEmptyArrayTest()
    {
        int[] numbers = [];
        var permutations = numbers.Permutations().ToList();
        
        permutations.Should().HaveCount(1);
        permutations.Should().Contain(Array.Empty<int>());
    }

    [Fact(DisplayName = "Permutations should generate correct count for larger array")]
    public void PermutationsLargerArrayTest()
    {
        int[] numbers = [1, 2, 3, 4];
        var permutations = numbers.Permutations().ToList();
        
        permutations.Should().HaveCount(24); // 4! = 24
    }

    [Fact(DisplayName = "Permutations should not modify original array")]
    public void PermutationsDoesNotModifyOriginalTest()
    {
        int[] numbers = [1, 2, 3];
        int[] original = [1, 2, 3];
        
        _ = numbers.Permutations().ToList();
        
        numbers.Should().Equal(original);
    }
}

