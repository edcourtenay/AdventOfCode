using AdventOfCode.Year2016;

using FluentAssertions;

namespace AdventOfCode.Tests.Year2016;

public class Day04Tests
{
    private string testData = """
    aaaaa-bbb-z-y-x-123[abxyz]
    a-b-c-d-e-f-g-h-987[abcde]
    not-a-real-room-404[oarel]
    totally-real-room-200[decoy]
    """;

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        var sut = new Day04();

        sut.Part1(testData).As<int>().Should().Be(1514);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        
    }
}