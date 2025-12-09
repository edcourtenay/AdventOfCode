using System.Diagnostics.CodeAnalysis;

using AdventOfCode.Solutions.Extensions;

namespace AdventOfCode.Tests.Extensions;

[SuppressMessage("Performance", "CA1861:Avoid constant arrays as arguments")]
public class CombinatorialExtensionsTest
{
    [Fact(DisplayName = "Combinations should generate all combinations of width 2")]
    public void CombinationsWidth2Test()
    {
        int[] numbers = [1, 2, 3, 4];
        var combinations = numbers.Combinations(2).ToList();
        
        combinations.Should().HaveCount(6); // C(4,2) = 6
        combinations.Should().ContainEquivalentOf(new[] { 1, 2 });
        combinations.Should().ContainEquivalentOf(new[] { 1, 3 });
        combinations.Should().ContainEquivalentOf(new[] { 1, 4 });
        combinations.Should().ContainEquivalentOf(new[] { 2, 3 });
        combinations.Should().ContainEquivalentOf(new[] { 2, 4 });
        combinations.Should().ContainEquivalentOf(new[] { 3, 4 });
    }

    [Fact(DisplayName = "Combinations should generate all combinations of width 3")]
    public void CombinationsWidth3Test()
    {
        int[] numbers = [1, 2, 3, 4];
        var combinations = numbers.Combinations(3).ToList();
        
        combinations.Should().HaveCount(4); // C(4,3) = 4
        combinations.Should().ContainEquivalentOf(new[] { 1, 2, 3 });
        combinations.Should().ContainEquivalentOf(new[] { 1, 2, 4 });
        combinations.Should().ContainEquivalentOf(new[] { 1, 3, 4 });
        combinations.Should().ContainEquivalentOf(new[] { 2, 3, 4 });
    }

    [Fact(DisplayName = "Combinations of width 1 should return all elements")]
    public void CombinationsWidth1Test()
    {
        int[] numbers = [1, 2, 3];
        var combinations = numbers.Combinations(1).ToList();
        
        combinations.Should().HaveCount(3);
        combinations.Should().ContainEquivalentOf(new[] { 1 });
        combinations.Should().ContainEquivalentOf(new[] { 2 });
        combinations.Should().ContainEquivalentOf(new[] { 3 });
    }

    [Fact(DisplayName = "Combinations of full width should return one combination")]
    public void CombinationsFullWidthTest()
    {
        int[] numbers = [1, 2, 3];
        var combinations = numbers.Combinations(3).Select(x => x.ToArray()).ToList();
        
        combinations.Should().HaveCount(1);
        combinations.Should().ContainEquivalentOf(new[] { 1, 2, 3 });
    }

    [Fact(DisplayName = "Combinations should handle empty result")]
    public void CombinationsEmptyResultTest()
    {
        int[] numbers = [1, 2];
        var combinations = numbers.Combinations(3).ToList();
        
        combinations.Should().BeEmpty();
    }

    [Fact(DisplayName = "Combinations should work with strings")]
    public void CombinationsWithStringsTest()
    {
        string[] letters = ["A", "B", "C"];
        var combinations = letters.Combinations(2).Select(c => c.ToArray()).ToList();
        
        combinations.Should().HaveCount(3);
        combinations.Should().ContainEquivalentOf(new[] { "A", "B" });
        combinations.Should().ContainEquivalentOf(new[] { "A", "C" });
        combinations.Should().ContainEquivalentOf(new[] { "B", "C" });
    }
    
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

