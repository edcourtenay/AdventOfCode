using AdventOfCode.Year2018;

namespace AdventOfCode.Tests.Year2018;

public class Day01Tests : IClassFixture<Day01>
{
    private readonly Day01 _sut;

    private const string TestData = """
    +3
    +3
    +4
    -2
    -4
    """;

    public Day01Tests(Day01 sut)
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
        _sut.Part2(TestData).As<int>().Should().Be(10);
    }
}