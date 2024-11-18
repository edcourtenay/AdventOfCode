using AdventOfCode.Solutions.Year2023;

namespace AdventOfCode.Tests.Year2023;

public class Day12Tests : IClassFixture<Day12>
{
    private const string TestData =
        """
        ???.### 1,1,3
        .??..??...?##. 1,1,3
        ?#?#?#?#?#?#?#? 1,3,1,6
        ????.#...#... 4,1,1
        ????.######..#####. 1,6,5
        ?###???????? 3,2,1
        """;

    private readonly Day12 _sut;

    public Day12Tests(Day12 sut)
    {
        _sut = sut;
    }

    [Theory(DisplayName = "Part1 should return expected results from example data")]
    [InlineData(TestData, 21)]
    public void Part1Example(string input, int expected)
    {
        _sut.Part1(input).As<long>().Should().Be(expected);
    }

    [Theory(DisplayName = "Part2 should return expected results from example data")]
    [InlineData(TestData, 525152)]
    public void Part2Example(string input, int expected)
    {
        _sut.Part2(input).As<long>().Should().Be(expected);
    }

    [Theory(DisplayName = "CalculateLegalPositions should return expected results from example data")]
    [InlineData("???.###", new[] { 1, 1, 3 }, 1)]
    [InlineData(".??..??...?##.", new[] { 1, 1, 3 }, 4)]
    [InlineData("?#?#?#?#?#?#?#?", new[] { 1, 3, 1, 6 }, 1)]
    [InlineData("????.#...#...", new[] { 4, 1, 1 }, 1)]
    [InlineData("????.######..#####.", new[] { 1, 6, 5 }, 4)]
    [InlineData("?###????????", new[] { 3, 2, 1 }, 10)]
    public void CalculateLegalPositionsExample(string input, int[] springLengths, long expected)
    {
        Day12.CalculateLegalPositions(input, springLengths).Should().Be(expected);
    }
}