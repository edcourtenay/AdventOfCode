using AdventOfCode.Year2022;

using FluentAssertions;

namespace AdventOfCode.Tests.Year2022;

public class Day02Tests : IClassFixture<Day02>
{
    private const string TestData = """
    A Y
    B X
    C Z
    """;

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var sut = new Day02();

        sut.Part1(TestData).As<int>().Should().Be(15);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        var sut = new Day02();

        sut.Part2(TestData).As<int>().Should().Be(12);
    }
}