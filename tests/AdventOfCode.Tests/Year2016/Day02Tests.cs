using AdventOfCode.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day02Tests
{
    private const string TestData = """
                                    ULL
                                    RRDDD
                                    LURDL
                                    UUUUD
                                    """;

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var sut = new Day02();

        sut.Part1(TestData).As<string>().Should().Be("1985");
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        var sut = new Day02();

        sut.Part2(TestData).As<string>().Should().Be("5DB3");
    }
}