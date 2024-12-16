using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day08Tests : IClassFixture<Day08>
{
    private readonly Day08 _sut;

    private const string TestData =
        """
        ............
        ........0...
        .....0......
        .......0....
        ....0.......
        ......A.....
        ............
        ............
        ........A...
        .........A..
        ............
        ............
        """;

    public Day08Tests(Day08 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(14);
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(34);
    }
}
