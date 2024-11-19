using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day01Tests : IClassFixture<Day01>
{
    private readonly Day01 _sut;

    private const string TestData =
        """
        3   4
        4   3
        2   5
        1   3
        3   9
        3   3
        """;

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(11);
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(31);
    }
}
