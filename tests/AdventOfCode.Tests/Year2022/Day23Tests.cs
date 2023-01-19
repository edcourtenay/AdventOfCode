using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day23Tests : IClassFixture<Day23>
{
    private readonly Day23 _sut;

    private const string TestData = """
    ....#..
    ..###.#
    #...#.#
    .#...##
    #.###..
    ##.#.##
    .#..#..
    """;

    public Day23Tests(Day23 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(110);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(301);
    }
}