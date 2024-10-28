using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day11Tests : IClassFixture<Day11>
{
    private readonly Day11 _sut;

    private const string TestData =
        """
        ...#......
        .......#..
        #.........
        ..........
        ......#...
        .#........
        .........#
        ..........
        .......#..
        #...#.....
        """;

    public Day11Tests(Day11 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 2, 374)]
    [InlineData(TestData, 10, 1030)]
    [InlineData(TestData, 100, 8410)]
    public void SolveExample(string input, long expansionFactor, long expected)
    {
        Day11.Solve(input, expansionFactor).As<long>().Should().Be(expected);
    }

    [Fact(DisplayName = "Parse should return expected results from example data")]
    public void ParseTest()
    {
        Day11.Parse(TestData).Should().BeEquivalentTo([
            (3, 0), (7, 1), (0, 2), (6, 4), (1, 5), (9, 6), (7, 8), (0, 9), (4, 9)
        ]);
    }

    [Fact(DisplayName = "Expand should return expected results from example data")]
    public void ExpandTest()
    {
        Day11.Expand(new (long, long)[]
        {
            (3, 0), (7, 1), (0, 2), (6, 4), (1, 5), (9, 6), (7, 8), (0, 9), (4, 9)
        }, 2L).Should().BeEquivalentTo([
            (4, 0), (9, 1), (0, 2), (8, 5), (1, 6), (12, 7), (9, 10), (0, 11), (5, 11)
        ]);
    }
}