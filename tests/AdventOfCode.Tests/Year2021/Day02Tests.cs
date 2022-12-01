using AdventOfCode.Year2021;

using FluentAssertions;

namespace AdventOfCode.Tests.Year2021;

public class Day02Tests
{
    private const string TestData = """
    forward 5
    down 5
    forward 8
    up 3
    down 8
    forward 2
    """;

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var sut = new Day02();

        sut.Part1(TestData).As<int>().Should().Be(150);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        var sut = new Day02();

        sut.Part2(TestData).As<int>().Should().Be(900);
    }
}