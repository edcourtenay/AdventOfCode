using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day05Tests
{
    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        Day05? sut = new();

        sut.Part1("abc").As<string>().Should().Be("18f47a30");
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        Day05? sut = new();

        sut.Part2("abc").As<string>().Should().Be("05ace8e3");
    }
}