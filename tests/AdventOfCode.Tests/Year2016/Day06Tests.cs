using AdventOfCode.Solutions.Year2016;

namespace AdventOfCode.Tests.Year2016;

public class Day06Tests : IClassFixture<Day06>
{
    private const string TestData = """
                                    eedadn
                                    drvtee
                                    eandsr
                                    raavrd
                                    atevrs
                                    tsrnev
                                    sdttsa
                                    rasrtv
                                    nssdts
                                    ntnada
                                    svetve
                                    tesnvt
                                    vntsnd
                                    vrdear
                                    dvrsen
                                    enarar
                                    """;

    private readonly Day06 _sut;


    public Day06Tests(Day06 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Part1 should return expected results from example data")]
    public void Part1Example()
    {
        _sut.Part1(TestData).As<string>().Should().Be("easter");
    }

    [Fact(DisplayName = "Part2 should return expected results from example data")]
    public void Part2Example()
    {
        _sut.Part2(TestData).As<string>().Should().Be("advent");
    }
}