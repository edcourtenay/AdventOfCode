using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day06Tests : IClassFixture<Day06>
{
    private readonly Day06 _sut;

    private const string TestData = """
                                    123 328  51 64 
                                     45 64  387 23 
                                      6 98  215 314
                                    *   +   *   +  
                                    """;

    public Day06Tests(Day06 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Test()
    {
        _sut.Part1(TestData).As<long>().Should().Be(4277556L);
    }

    [Fact]
    public void Part2Test()
    {
        _sut.Part2(TestData).As<long>().Should().Be(3263827L);
    }
}