using System.Text.Json.Nodes;

namespace AdventOfCode.Solutions.Year2022;

[Description("Distress Signal")]
public class Day13 : IPuzzle
{
    public object Part1(string input)
    {
        return Enumerable
            .Select<IEnumerable<JsonNode>, JsonNode[]>(ToPackets(input)
                .Window(2), nodes => Enumerable.ToArray<JsonNode>(nodes))
            .Select((pair, index) => CompareNodes(pair[0], pair[1]) < 0 ? index + 1 : 0)
            .Sum();
    }

    public object Part2(string input)
    {
        JsonNode divider1 = JsonNode.Parse("[[2]]")!;
        JsonNode divider2 = JsonNode.Parse("[[6]]")!;

        var packets = ToPackets(input).Append(divider1).Append(divider2).ToList();

        packets.Sort(CompareNodes);
        return (packets.IndexOf(divider1) + 1) * (packets.IndexOf(divider2) + 1);
    }

    static IEnumerable<JsonNode> ToPackets(string input)
    {
        return Enumerable
            .Where<string>(input.ToLines(), line => !string.IsNullOrEmpty(line)).Select(line => JsonNode.Parse(line)!);
    }

    static int CompareNodes(JsonNode? left, JsonNode? right)
    {
        return (left, right) switch
        {
            (JsonValue lv, JsonValue rv) => lv.GetValue<int>().CompareTo(rv.GetValue<int>()),
            (JsonValue lv, JsonArray ra) => CompareNodes(new JsonArray(lv.GetValue<int>()), ra),
            (JsonArray la, JsonValue rv) => CompareNodes(la, new JsonArray(rv.GetValue<int>())),
            (JsonArray la, JsonArray ra) => CompareArrays(la, ra),
            _ => throw new ArgumentOutOfRangeException()
        };

        int CompareArrays(JsonArray leftArray, JsonArray rightArray)
        {
            var c = leftArray.Zip(rightArray)
                .Select(tuple => CompareNodes(tuple.First, tuple.Second))
                .FirstOrDefault(i => i != 0);

            return c != 0 ? c : leftArray.Count.CompareTo(rightArray.Count);
        }
    }
}