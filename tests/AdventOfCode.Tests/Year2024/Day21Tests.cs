using AdventOfCode.Solutions.Year2024;

namespace AdventOfCode.Tests.Year2024;

public class Day21Tests : IClassFixture<Day21>
{
    private readonly Day21 _sut;

    private const string TestData = "";

    public Day21Tests(Day21 sut)
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
