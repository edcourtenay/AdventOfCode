using AdventOfCode.Year2022;

using FluentAssertions;

namespace AdventOfCode.Tests.Year2022;

public class Day08Tests : IClassFixture<Day08>
{
    private readonly Day08 _sut;

    private const string TestData = """
    30373
    25512
    65332
    33549
    35390
    """;

    public Day08Tests(Day08 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(21);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(8);
    }

}