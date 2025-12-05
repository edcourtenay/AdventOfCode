using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day05Tests : IClassFixture<Day05>
{
    private readonly Day05 _sut;

    private const string TestData = """
                                    3-5
                                    10-14
                                    16-20
                                    12-18

                                    1
                                    5
                                    8
                                    11
                                    17
                                    32
                                    """;

    public Day05Tests(Day05 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Test()
    {
        _sut.Part1(TestData).As<int>().Should().Be(3);
    }

    [Fact]
    public void Part2Test()
    {
        _sut.Part2(TestData).As<ulong>().Should().Be(14);
    }
}