using AdventOfCode.Year2017;

namespace AdventOfCode.Tests.Year2017;

public class Day07Tests : IClassFixture<Day07>
{
    private readonly Day07 _sut;

    private static readonly string ExampleData =
        """
        pbga (66)
        xhth (57)
        ebii (61)
        havc (66)
        ktlj (57)
        fwft (72) -> ktlj, cntj, xhth
        qoyq (66)
        padx (45) -> pbga, havc, qoyq
        tknk (41) -> ugml, padx, fwft
        jptl (61)
        ugml (68) -> gyxo, ebii, jptl
        gyxo (61)
        cntj (57)
        """;

    public Day07Tests(Day07 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(ExampleData).As<string>().Should().Be("tknk");
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(ExampleData).As<int>().Should().Be(60);
    }
}