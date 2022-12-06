using AdventOfCode.Year2020;

namespace AdventOfCode.Tests.Year2020;

public class Day06Tests : IClassFixture<Day06>
{
    private readonly Day06 _sut;

    private const string TestData = """
    abc

    a
    b
    c

    ab
    ac

    a
    a
    a
    a

    b
    """;

    public Day06Tests(Day06 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(11);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(6);
    }
}