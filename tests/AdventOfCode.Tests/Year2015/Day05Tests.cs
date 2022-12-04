using AdventOfCode.Year2015;

namespace AdventOfCode2015CS.Tests;

public class Day05Tests
{
    [Theory(DisplayName = "HasThreeVowels returns expected results")]
    [InlineData("aei", true)]
    [InlineData("xazegov", true)]
    [InlineData("ugknbfddgicrmopn", true)]
    [InlineData("aaa", true)]
    [InlineData("xyz", false)]
    public void HasThreeVowelsTest(string text, bool expected)
    {
        var result = Day05.HasThreeVowels(text);

        result.Should().Be(expected);
    }

    [Theory(DisplayName = "HasRepeatedLetter returns expected results")]
    [InlineData("aaab", true)]
    [InlineData("xyz", false)]
    public void HasRepeatedLetterTest(string text, bool expected)
    {
        var result = Day05.HasRepeatedLetter(text);

        result.Should().Be(expected);
    }

    [Theory(DisplayName = "HasDisallowedSubstring returns expected results")]
    [InlineData("haegwjzuvuyypxyu", true)]
    [InlineData("aaa", false)]
    public void HasDisallowedSubstringTest(string text, bool expected)
    {
        var result = Day05.HasDisallowedSubstring(text);

        result.Should().Be(expected);
    }

    [Theory(DisplayName = "Naughty or Nice tests")]
    [InlineData("ugknbfddgicrmopn", true)]
    [InlineData("aaa", true)]
    [InlineData("jchzalrnumimnmhp", false)]
    [InlineData("haegwjzuvuyypxyu", false)]
    [InlineData("dvszwmarrgswjxmb", false)]
    public void NiceTests(string text, bool expected)
    {
        var result = Day05.NiceTest(text);

        result.Should().Be(expected);
    }
}