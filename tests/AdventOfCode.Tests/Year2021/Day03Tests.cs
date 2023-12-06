using AdventOfCode.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class Day03Tests : IClassFixture<Day03>
{
    private readonly Day03 _sut;

    private const string TestData = 
        """
        00100
        11110
        10110
        10111
        10101
        01111
        00111
        11100
        10000
        11001
        00010
        01010
        """;

    public Day03Tests(Day03 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(198);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(230);
    }
}