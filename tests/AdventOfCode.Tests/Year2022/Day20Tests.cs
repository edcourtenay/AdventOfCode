using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day20Tests : IClassFixture<Day20>
{
    private readonly Day20 _sut;

    private const string TestData = """
    1
    2
    -3
    3
    -2
    0
    4
    """;

    public Day20Tests(Day20 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<long>().Should().Be(3);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<long>().Should().Be(1623178306L);
    }
}