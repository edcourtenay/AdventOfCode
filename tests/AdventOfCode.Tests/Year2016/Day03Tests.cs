using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day03Tests : IClassFixture<Day03>
{
    private readonly Day03 _sut;

    public Day03Tests(Day03 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1("5 10 25").As<int>().Should().Be(0);
    }
}