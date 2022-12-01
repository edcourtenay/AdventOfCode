using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode.Year2016;

[Description("How About a Nice Game of Chess?")]
public class Day05 : IPuzzle
{
    private readonly MD5 _md5 = MD5.Create();

    public object Part1(string input)
    {
        int i = 0;
        long result = 0;

        for (int j = 0; j < 8; j++)
        {
            byte[] hash;
            do
            {
                string chars = $"{input}{i++}";
                hash = _md5.ComputeHash(Encoding.UTF8.GetBytes(chars));
            } while (!FiveZeroes(hash));

            result = (result << 4) | (byte)(hash[2] & 0x0F);
        }

        return result.ToString("x8");
    }

    public object Part2(string input)
    {
        int i = 0;
        ulong result = 0;
        HashSet<int> seen = new();

        for (int j = 0; j < 8; j++)
        {
            byte[] hash;
            byte pos;
            do
            {
                string chars = $"{input}{i++}";
                hash = _md5.ComputeHash(Encoding.UTF8.GetBytes(chars));
                pos = (byte)(hash[2] & 0x0F);
            } while (!FiveZeroes(hash) || seen.Contains(pos));

            byte val = (byte)((hash[3] >> 4) & 0x0F);
            result |= (uint)(val << (4 * (8 - (pos + 1))));
            result = result & 0xFFFFFFFF;
            seen.Add(pos);
        }

        return result.ToString("x8");
    }

    public static readonly Func<byte[], bool> FiveZeroes = bytes => bytes[0] == 0 && bytes[1] == 0 && (bytes[2] & 0xF0) == 0;
}