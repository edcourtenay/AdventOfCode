using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day07Tests : IClassFixture<Day07>
{
    private readonly Day07 _sut;

    private const string Data = """
                                123 -> x
                                456 -> y
                                x AND y -> d
                                x OR y -> e
                                x LSHIFT 2 -> f
                                y RSHIFT 2 -> g
                                NOT x -> h
                                NOT y -> i
                                x -> aa
                                """;

    private static readonly Dictionary<string, string> ParsedDictionary = new()
    {
        { "x", "123" },
        { "y", "456" },
        { "d", "x AND y" },
        { "e", "x OR y" },
        { "f", "x LSHIFT 2" },
        { "g", "y RSHIFT 2" },
        { "h", "NOT x" },
        { "i", "NOT y" },
        { "aa", "x" }
    };

    public Day07Tests(Day07 sut)
    {
        _sut = sut;
    }

    [Fact(DisplayName = "Data should produce a known dictionary")]
    public void TestData()
    {
        IDictionary<string, string> dictionary = _sut.Data(Data);

        dictionary.Should().Equal(ParsedDictionary);

        Dictionary<string, ushort>
            d = dictionary.Keys.ToDictionary(k => k, k => (ushort)_sut.EvaluateKey(dictionary, k));
        d.Should().NotBeNull();
    }
}