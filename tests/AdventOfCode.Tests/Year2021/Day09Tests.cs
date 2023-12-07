using AdventOfCode.Year2021;

namespace AdventOfCode.Tests.Year2021;

public class Day09Tests : IClassFixture<Day09>
{
    private readonly Day09 _sut;

    private const string TestData =
        """
        2199943210
        3987894921
        9856789892
        8767896789
        9899965678
        """;

    public Day09Tests(Day09 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(15);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(1134);
    }
}