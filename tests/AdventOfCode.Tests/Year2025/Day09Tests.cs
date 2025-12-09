using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day09Tests : IClassFixture<Day09>
{
    private readonly Day09 _sut;

    private const string TestData =
        """
        7,1
        11,1
        11,7
        9,7
        9,5
        2,5
        2,3
        7,3
        """;

    public Day09Tests(Day09 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Test()
    {
        _sut.Part1(TestData).As<long>().Should().Be(50);
    }

    [Fact]
    public void Part2Test()
    {
        _sut.Part2(TestData).As<long>().Should().Be(24);
    }
}