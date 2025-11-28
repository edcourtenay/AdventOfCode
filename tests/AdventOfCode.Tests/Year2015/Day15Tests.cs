using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day15Tests : IClassFixture<Day15>
{
    private readonly Day15 _sut;

    const string part1Data = """
                             Butterscotch: capacity -1, durability -2, flavor 6, texture 3, calories 8
                             Cinnamon: capacity 2, durability 3, flavor -2, texture -1, calories 3
                             """;

    public Day15Tests(Day15 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Test()
    {
        long result = (long)_sut.Part1(part1Data);

        result.Should().Be(62842880L);
    }

    [Fact]
    public void Part2Test()
    {
        long result = (long)_sut.Part2(part1Data);

        result.Should().Be(57600000L);
    }
}