using AdventOfCode.Year2016;

using FluentAssertions;

namespace AdventOfCode.Tests.Year2016;

public class Day05Tests
{
    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var sut = new Day05();

        sut.Part1("abc").As<string>().Should().Be("18f47a30");
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        var sut = new Day05();

        sut.Part2("abc").As<string>().Should().Be("05ace8e3");
    }
}