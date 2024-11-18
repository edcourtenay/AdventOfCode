using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2016;

[Description("Security Through Obscurity")]
public partial class Day04 : IPuzzle
{
    private readonly Regex _lineRegex = LineRegex();

    public object Part1(string input)
    {
        return GetValidRooms(input)
            .Select(tuple => tuple.sectorid)
            .Sum();
    }

    public object Part2(string input)
    {
        return GetValidRooms(input)
            .Select(tuple => (room: Decrypt(tuple.room, tuple.sectorid), tuple.sectorid))
            .First(tuple => tuple.room == "northpole object storage").sectorid;
    }

    private string Decrypt(string tupleRoom, int tupleSectorid)
    {
        char[] chars = tupleRoom.ToCharArray();
        var rotated = chars.Select(c => c switch
        {
            '-' => ' ',
            >= 'a' and <= 'z' => RotateChar(c, tupleSectorid),
            _ => c
        }).ToArray();
        return new string(rotated);
    }

    private char RotateChar(char c, int tupleSectorid)
    {
        return (char)((((c - 'a') + tupleSectorid) % 26) + 'a');
    }

    private IEnumerable<(string room, int sectorid, string checksum)> GetValidRooms(string input)
    {
        return Enumerable
            .Select<string, Match>(input.ToLines(), s => _lineRegex.Match(s))
            .Where(m => m.Success)
            .Select(m => (room: m.Groups["room"].Value, sectorid: int.Parse(m.Groups["sectorid"].Value),
                checksum: m.Groups["checksum"].Value))
            .Where(tuple => tuple.checksum == CalculateChecksum(tuple.room));
    }

    private static string CalculateChecksum(string line)
    {
        return new string(line
            .Where(c => c != '-')
            .GroupBy(c => c)
            .Select(chars => (key: chars.Key, count: chars.Count()))
            .OrderByDescending(tuple => tuple.count)
            .ThenBy(tuple => tuple.key)
            .Take(5)
            .Select(tuple => tuple.key)
            .ToArray());
    }

    [GeneratedRegex(@"^(?<room>.*?)-(?<sectorid>\d+)\[(?<checksum>\w+)\]$", RegexOptions.Compiled)]
    private static partial Regex LineRegex();
}