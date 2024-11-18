using AdventOfCode.Solutions.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day21Tests : IClassFixture<Day21>
{
    private const string TestData =
        """
        ...........
        .....###.#.
        .###.##..#.
        ..#.#...#..
        ....#.#....
        .##..S####.
        .##..#...#.
        .......##..
        .##.#.####.
        .##..##.##.
        ...........
        """;

    private readonly Day21 _sut;

    public Day21Tests(Day21 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 6, 16)]
    public void Part1Example(string input, int targetSteps, int expected)
    {
        Day21.Solve(input, targetSteps).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 6, 16)]
    [InlineData(TestData, 10, 50)]
    [InlineData(TestData, 50, 1594)]
    public void Part2Example(string input, int targetSteps, int expected)
    {
        Day21.Solve(input, targetSteps).As<int>().Should().Be(expected);
    }
}