using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day11Tests : IClassFixture<Day11>
{
    private readonly Day11 _sut;

    public Day11Tests(Day11 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "IncrementPassword should increment correctly")]
    [InlineData("aaa", "aab")]
    [InlineData("azz", "baa")]
    public void IncrementPasswordTests(string password, string expected)
    {
        _sut.IncrementPassword(password.ToCharArray()).Should().ContainInOrder(expected.ToCharArray());
    }

    [Theory(DisplayName = "HasRunOfCharacters should evaluate correctly")]
    [InlineData("zxabcio", true)]
    [InlineData("abdeghp", false)]
    public void HasRunOfCharactersTests(string input, bool expected)
    {
        bool result = Day11.HasRunOfCharacters(input.ToCharArray());

        result.Should().Be(expected);
    }

    [Theory]
    [InlineData("abbceffg", true)]
    [InlineData("hijklmmn", false)]
    public void HasNonOverlappingPairsTests(string input, bool expected)
    {
        bool result = Day11.HasNonOverlappingPairs(input.ToCharArray());

        result.Should().Be(expected);
    }
}