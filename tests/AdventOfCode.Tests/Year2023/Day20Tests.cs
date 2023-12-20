using AdventOfCode.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day20Tests : IClassFixture<Day20>
{
    private readonly Day20 _sut;

    private const string TestData =
        """
        broadcaster -> a
        %a -> inv, con
        &inv -> b
        %b -> con
        &con -> output
        """;

    public Day20Tests(Day20 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 11687500)]
    public void Part1Example(string input, long expected)
    {
        _sut.Part1(input).As<long>().Should().Be(expected);
    }
}