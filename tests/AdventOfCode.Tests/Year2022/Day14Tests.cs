using AdventOfCode.Solutions.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day14Tests : IClassFixture<Day14>
{
    private const string TestData = """
                                    498,4 -> 498,6 -> 496,6
                                    503,4 -> 502,4 -> 502,9 -> 494,9
                                    """;

    private readonly Day14 _sut;

    public Day14Tests(Day14 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(24);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(93);
    }
}