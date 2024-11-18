using AdventOfCode.Solutions.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day18Tests : IClassFixture<Day18>
{
    private const string TestData =
        """
        R 6 (#70c710)
        D 5 (#0dc571)
        L 2 (#5713f0)
        D 2 (#d2c081)
        R 2 (#59c680)
        D 2 (#411b91)
        L 5 (#8ceee2)
        U 2 (#caa173)
        L 1 (#1b58a2)
        U 2 (#caa171)
        R 2 (#7807d2)
        U 3 (#a77fa3)
        L 2 (#015232)
        U 2 (#7a21e3)
        """;

    private readonly Day18 _sut;

    public Day18Tests(Day18 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 62)]
    public void Part1Example(string input, long expected)
    {
        _sut.Part1(input).As<long>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 952408144115)]
    public void Part2Example(string input, long expected)
    {
        _sut.Part2(input).As<long>().Should().Be(expected);
    }

    [Fact(DisplayName = "Shoelace test")]
    public void ShoelaceAreaTest()
    {
        ((int x, int y) from, (int x, int y) to)[] edges =
        [
            ((0, 0), (4, 0)), ((4, 0), (4, 4)), ((4, 4), (0, 4)), ((0, 4), (0, 0))
        ];

        int result =
            Math.Abs(
                edges.Aggregate(0, (acc, edge) => acc += (edge.from.x + edge.to.x) * (edge.from.y - edge.to.y)) / 2);

        result.Should().Be(16);
    }
}