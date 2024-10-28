using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day17Tests : IClassFixture<Day17>
{
    private readonly Day17 _sut;

    private const string TestData =
        """
        2413432311323
        3215453535623
        3255245654254
        3446585845452
        4546657867536
        1438598798454
        4457876987766
        3637877979653
        4654967986887
        4564679986453
        1224686865563
        2546548887735
        4322674655533
        """;

    private const string TestData2 =
        """
        111111111111
        999999999991
        999999999991
        999999999991
        999999999991
        """;

    public Day17Tests(Day17 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 102)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 94)]
    [InlineData(TestData2, 71)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}