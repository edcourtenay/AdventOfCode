using AdventOfCode.Year2022;

using FluentAssertions;

namespace AdventOfCode.Tests.Year2022;

public class Day04Tests : IClassFixture<Day04>
{
    private readonly Day04 _sut;

    private const string TestData = """
    Insert Test Data Here
    """;

    public Day04Tests(Day04 sut)
    {
        _sut = sut;
    }
    
    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(-1);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(-1);
    }
}