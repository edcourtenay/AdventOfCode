using AdventOfCode.Year2018;

namespace AdventOfCode.Tests.Year2018;

public class Day03Tests : IClassFixture<Day03>
{
    private readonly Day03 _sut;

    private const string TestData = """
    #1 @ 1,3: 4x4
    #2 @ 3,1: 4x4
    #3 @ 5,5: 2x2
    """;

    public Day03Tests(Day03 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(4);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(3);
    }
}