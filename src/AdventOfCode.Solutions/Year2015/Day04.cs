using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Solutions.Year2015;

[SuppressMessage("ReSharper", "UnusedType.Global")]
[Description("The Ideal Stocking Stuffer")]
public class Day04 : IPuzzle
{
    private static readonly MD5 MD5 = MD5.Create();

    public object Part1(string input)
    {
        _findSeed = FindSeed(input, Part1Test);
        return _findSeed;
    }

    public object Part2(string input)
    {
        return FindSeed(_findSeed?.ToString() ?? input, Part2Test);
    }

    public int FindSeed(string prefix, Func<byte[], bool> func)
    {
        int i = 0;
        while (!CheckMD5(func, Encoding.ASCII.GetBytes($"{prefix}{++i}")))
        {
        }

        return i;
    }

    public bool CheckMD5(Func<byte[], bool> func, byte[] bytes)
    {
        byte[] hash = MD5.ComputeHash(bytes);

        return func(hash);
    }

    public static readonly Func<byte[], bool> Part1Test = bytes => bytes[0] == 0 && bytes[1] == 0 && (bytes[2] & 0xF0) == 0;
    public static readonly Func<byte[], bool> Part2Test = bytes => bytes[0] == 0 && bytes[1] == 0 && bytes[2] == 0;
    private int? _findSeed;
}