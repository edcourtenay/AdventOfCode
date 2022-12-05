using AdventOfCode.Year2019;

namespace AdventOfCode.Tests.Year2019;

public class Day01Tests : IClassFixture<Day01>
{
    private readonly Day01 _sut;

    private const string TestData = """
    12
    14
    1969
    100756
    """;

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(34241);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(51316);
    }
}