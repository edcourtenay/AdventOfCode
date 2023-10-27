using AdventOfCode.Year2017;

namespace AdventOfCode.Tests.Year2017;

public class Day05Tests : IClassFixture<Day05>
{
    private const string StepsInput = "0\n3\n0\n1\n-3";
    private readonly Day05 _sut;

    public Day05Tests(Day05 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(StepsInput).As<int>().Should().Be(5);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(StepsInput).As<int>().Should().Be(10);
    }

}