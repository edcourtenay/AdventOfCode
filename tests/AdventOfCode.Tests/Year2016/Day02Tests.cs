using AdventOfCode.Year2016;

using FluentAssertions;

namespace AdventOfCode.Tests.Year2016;

public class Day02Tests
{
    private string testData = """
    ULL
    RRDDD
    LURDL
    UUUUD
    """;

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var sut = new Day02();

        sut.Part1(testData).As<string>().Should().Be("1985");
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        var sut = new Day02();

        sut.Part2(testData).As<string>().Should().Be("5DB3");
    }
}