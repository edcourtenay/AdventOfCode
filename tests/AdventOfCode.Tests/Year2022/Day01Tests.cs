using AdventOfCode.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day01Tests
{
    private const string TestData = """
    1000
    2000
    3000

    4000

    5000
    6000

    7000
    8000
    9000

    10000
    """;

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var sut = new Day01();

        sut.Part1(TestData).As<int>().Should().Be(24000);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        var sut = new Day01();

        sut.Part2(TestData).As<int>().Should().Be(45000);
    }
}