using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day15Tests : IClassFixture<Day15>
{
    private readonly Day15 _sut;

    private const string TestData = "";

    public Day15Tests(Day15 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(TestData).Should().Be("");
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(TestData).Should().Be("");
    }
}
