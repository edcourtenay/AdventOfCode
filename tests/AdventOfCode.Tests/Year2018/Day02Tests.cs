using AdventOfCode.Solutions.Year2018;

namespace AdventOfCode.Tests.Year2018;

public class Day02Tests : IClassFixture<Day02>
{
    private const string Part1TestData = """
                                         abcdef
                                         bababc
                                         abbcde
                                         abcccd
                                         aabcdd
                                         abcdee
                                         ababab
                                         """;

    private const string Part2TestData = """
                                         abcde
                                         fghij
                                         klmno
                                         pqrst
                                         fguij
                                         axcye
                                         wvxyz
                                         """;

    private readonly Day02 _sut;

    public Day02Tests(Day02 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(Part1TestData).As<int>().Should().Be(12);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(Part2TestData).As<string>().Should().Be("fgij");
    }
}