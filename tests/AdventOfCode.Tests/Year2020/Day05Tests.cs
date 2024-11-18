using AdventOfCode.Solutions.Year2020;

namespace AdventOfCode.Tests.Year2020;

public class Day05Tests
{
    [Theory(DisplayName = "Parse should produce expected results")]
    [InlineData("FBFBBFFRLR", 357)]
    [InlineData("BFFFBBFRRR", 567)]
    [InlineData("FFFBBBFRRR", 119)]
    [InlineData("BBFFBBFRLL", 820)]
    public void ParseTests(string line, int seatId)
    {
        Day05.Parse(line).Should().Be(seatId);
    }
}