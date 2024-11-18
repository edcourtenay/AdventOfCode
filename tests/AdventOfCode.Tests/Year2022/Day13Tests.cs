using AdventOfCode.Solutions.Year2022;

namespace AdventOfCode.Tests.Year2022;

public class Day13Tests : IClassFixture<Day13>
{
    private const string TestData = """
                                    [1,1,3,1,1]
                                    [1,1,5,1,1]

                                    [[1],[2,3,4]]
                                    [[1],4]

                                    [9]
                                    [[8,7,6]]

                                    [[4,4],4,4]
                                    [[4,4],4,4,4]

                                    [7,7,7,7]
                                    [7,7,7]

                                    []
                                    [3]

                                    [[[]]]
                                    [[]]

                                    [1,[2,[3,[4,[5,6,7]]]],8,9]
                                    [1,[2,[3,[4,[5,6,0]]]],8,9]
                                    """;

    private readonly Day13 _sut;

    public Day13Tests(Day13 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<int>().Should().Be(13);
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<int>().Should().Be(140);
    }
}