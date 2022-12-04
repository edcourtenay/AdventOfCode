using AdventOfCode.Year2015;

namespace AdventOfCode2015CS.Tests;

public class Day11Tests
{
    [Theory(DisplayName = "IncrementPassword should increment correctly")]
    [InlineData("aaa", "aab")]
    [InlineData("azz", "baa")]
    public void IncrementPasswordTests(string password, string expected)
    {
        var sut = new Day11();

        var result = sut.IncrementPassword(password.ToCharArray());

        result.Should().ContainInOrder(expected.ToCharArray());
    }

    [Theory(DisplayName = "HasRunOfCharacters should evaluate correctly")]
    [InlineData("zxabcio", true)]
    [InlineData("abdeghp", false)]
    public void HasRunOfCharactersTests(string input, bool expected)
    {
        var result = Day11.HasRunOfCharacters(input.ToCharArray());

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("abbceffg", true)]
    [InlineData("hijklmmn", false)]
    public void HasNonOverlappingPairsTests(string input, bool expected)
    {
        var result = Day11.HasNonOverlappingPairs(input.ToCharArray());

        result.Should().Be(expected);
    }
}