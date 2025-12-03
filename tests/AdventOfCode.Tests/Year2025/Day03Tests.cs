using AdventOfCode.Solutions.Year2025;

namespace AdventOfCode.Tests.Year2025;

public class Day03Tests : IClassFixture<Day03>
{
    private readonly Day03 _sut;

    private const string ExampleData = """
                                       987654321111111
                                       811111111111119
                                       234234234234278
                                       818181911112111
                                       """;

    public Day03Tests(Day03 sut)
    {
        _sut = sut;
    }

    [Fact]
    public void Part1Example()
    {
        _sut.Part1(ExampleData).As<ulong>().Should().Be(357UL);
    }

    [Fact]
    public void Part2Example()
    {
        _sut.Part2(ExampleData).As<ulong>().Should().Be(3121910778619UL);
    }

    [Theory]
    [InlineData("987654321111111", 2,98)]
    [InlineData("811111111111119", 2,89)]
    [InlineData("234234234234278", 2,78)]
    [InlineData("818181911112111", 2,92)]
    [InlineData("987654321111111", 12,987654321111)]
    [InlineData("811111111111119", 12,811111111119)]
    [InlineData("234234234234278", 12,434234234278)]
    [InlineData("818181911112111", 12,888911112111)]
    public void CalculateJoltageTest(string bank, int length, ulong expected)
    {
        Day03.CalculateJoltage(bank, length).Should().Be(expected);
    }
}