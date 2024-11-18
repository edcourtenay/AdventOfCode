using AdventOfCode.Solutions.Year2017;

namespace AdventOfCode.Tests.Year2017;

public class Day08Tests : IClassFixture<Day08>
{
    private const string ExampleData = """
                                       b inc 5 if a > 1
                                       a inc 1 if b < 5
                                       c dec -10 if a >= 1
                                       c inc -20 if c == 10
                                       """;

    private readonly Day08 _sut;

    public Day08Tests(Day08 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(ExampleData).As<int>().Should().Be(1);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(ExampleData).As<int>().Should().Be(10);
    }
}