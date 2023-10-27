using AdventOfCode.Year2017;

namespace AdventOfCode.Tests.Year2017;

public class Day06Tests : IClassFixture<Day06>
{
    private readonly Day06 _sut;

    public Day06Tests(Day06 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1("0 2 7 0").As<int>().Should().Be(5);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2("2 4 1 2").As<int>().Should().Be(4);
    }
}