﻿using AdventOfCode.Solutions.Year2015;

namespace AdventOfCode.Tests.Year2015;

public class Day07Tests
{
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

    [Fact(DisplayName = "Data should produce a known dictionary")]
    public void TestData()
    {
        Day07 sut = new();

        IDictionary<string, string> dictionary = sut.Data(Data);

        dictionary.Should().Equal(ParsedDictionary);

        Dictionary<string, ushort>
            d = dictionary.Keys.ToDictionary(k => k, k => (ushort)sut.EvaluateKey(dictionary, k));
        d.Should().NotBeNull();
    }
}