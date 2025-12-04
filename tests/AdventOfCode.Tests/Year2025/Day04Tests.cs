using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day04Tests : IClassFixture<Day04>
{
    private readonly Day04 _sut;

    private const string ExampleData = """
                                       ..@@.@@@@.
                                       @@@.@.@.@@
                                       @@@@@.@.@@
                                       @.@@@@..@.
                                       @@.@@@@.@@
                                       .@@@@@@@.@
                                       .@.@.@.@@@
                                       @.@@@.@@@@
                                       .@@@@@@@@.
                                       @.@.@@@.@.
                                       """;

    public Day04Tests(Day04 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(ExampleData).As<int>().Should().Be(13);
    }

    [Fact]
    public void Part2Example()
    {
        _sut.Part2(ExampleData).As<int>().Should().Be(43);
    }
}