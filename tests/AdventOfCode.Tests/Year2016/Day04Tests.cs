using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day04Tests : IClassFixture<Day04>
{
    private readonly Day04 _sut;

    private const string TestData = """
                                    aaaaa-bbb-z-y-x-123[abxyz]
                                    a-b-c-d-e-f-g-h-987[abcde]
                                    not-a-real-room-404[oarel]
                                    totally-real-room-200[decoy]
                                    """;

    public Day04Tests(Day04 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(1514);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
    }
}