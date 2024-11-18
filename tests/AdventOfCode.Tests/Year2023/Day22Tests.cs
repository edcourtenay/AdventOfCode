using AdventOfCode.Solutions.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day22Tests : IClassFixture<Day22>
{
    private const string TestData =
        """
        1,0,1~1,2,1
        0,0,2~2,0,2
        0,2,3~2,2,3
        0,0,4~0,2,4
        2,0,5~2,2,5
        0,1,6~2,1,6
        1,1,8~1,1,9
        """;

    private const string TestData2 =
        """
        0,0,1~0,1,1
        1,1,1~1,1,1
        0,0,2~0,0,2
        0,1,2~1,1,2
        """;

    private readonly Day22 _sut;

    public Day22Tests(Day22 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 5)]
    [InlineData(TestData2, 3)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<int>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 7)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}