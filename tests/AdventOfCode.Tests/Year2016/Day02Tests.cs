using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day02Tests : IClassFixture<Day02>
{
    private readonly Day02 _sut;

    private const string TestData = """
                                    ULL
                                    RRDDD
                                    LURDL
                                    UUUUD
                                    """;

    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<string>().Should().Be("1985");
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<string>().Should().Be("5DB3");
    }
}