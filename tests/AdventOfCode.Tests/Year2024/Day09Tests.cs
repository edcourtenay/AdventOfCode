using AdventOfCode.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day09Tests : IClassFixture<Day09>
{
    private readonly Day09 _sut;

    private const string TestData = "";

    public Day09Tests(Day09 sut)
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
