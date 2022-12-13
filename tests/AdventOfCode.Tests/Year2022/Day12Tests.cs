using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day12Tests : IClassFixture<Day12>
{
    private readonly Day12 _sut;

    private const string TestData = """
    Sabqponm
    abcryxxl
    accszExk
    acctuvwj
    abdefghi
    """;

    public Day12Tests(Day12 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(31);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(29);
    }
}