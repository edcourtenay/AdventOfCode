using AdventOfCode.Solutions.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class Day06Tests : IClassFixture<Day06>
{
    private const string TestData = "3,4,3,1,2";
    private readonly Day06 _sut;

    public Day06Tests(Day06 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<long>().Should().Be(5934);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<long>().Should().Be(26984457539);
    }
}