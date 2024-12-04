using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day04Tests : IClassFixture<Day04>
{
    private readonly Day04 _sut;

    private const string TestData =
        """
        MMMSXXMASM
        MSAMXMSMSA
        AMXSXMAAMM
        MSAMASMSMX
        XMASAMXAMM
        XXAMMXXAMA
        SMSMSASXSS
        SAXAMASAAA
        MAMMMXMMMM
        MXMXAXMASX
        """;

    public Day04Tests(Day04 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(18);
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(9);
    }
}
