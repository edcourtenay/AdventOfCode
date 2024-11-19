using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day02Tests : IClassFixture<Day02>
{
    private readonly Day02 _sut;

    private const string TestData =
        """
        7 6 4 2 1
        1 2 7 8 9
        9 7 6 2 1
        1 3 2 4 5
        8 6 4 4 1
        1 3 6 7 9
        """;

    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(2);
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(4);
    }
}
