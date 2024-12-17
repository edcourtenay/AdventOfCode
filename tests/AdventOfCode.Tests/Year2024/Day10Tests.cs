using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day10Tests : IClassFixture<Day10>
{
    private readonly Day10 _sut;

    private const string TestData =
        """
        89010123
        78121874
        87430965
        96549874
        45678903
        32019012
        01329801
        10456732
        """;

    public Day10Tests(Day10 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<long>().Should().Be(36);
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<long>().Should().Be(81);
    }
}
