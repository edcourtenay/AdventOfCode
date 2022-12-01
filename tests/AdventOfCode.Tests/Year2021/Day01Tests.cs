using AdventOfCode.Year2021;

using FluentAssertions;

namespace AdventOfCode.Tests.Year2021;

public class Day01Tests
{
    private const string TestData = """
    199
    200
    208
    210
    200
    207
    240
    269
    260
    263
    """;

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var sut = new Day01();

        sut.Part1(TestData).As<int>().Should().Be(7);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        var sut = new Day01();

        sut.Part2(TestData).As<int>().Should().Be(5);
    }
}