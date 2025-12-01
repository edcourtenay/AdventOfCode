using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day01Tests : IClassFixture<Day01>
{
    private readonly Day01 _sut;

    private const string TestData =
        """
        L68
        L30
        R48
        L5
        R60
        L55
        L1
        L99
        R14
        L82
        """;

    public Day01Tests(Day01 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(3);
    }
    
    [Fact]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(6);
    }
    
    [Theory]
    [InlineData("R1000", 10)]
    [InlineData("L1000", 10)]
    public void WrapAroundTest(string input, int expected)
    {
        _sut.Part2(input).As<int>().Should().Be(expected);
    }
}