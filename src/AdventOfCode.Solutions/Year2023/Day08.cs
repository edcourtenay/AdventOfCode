namespace AdventOfCode.Solutions.Year2023;

[Description("Haunted Wasteland")]
public class Day08 : IPuzzle
{
    public object Part1(string input)
    {
        var map = Map.Parse(input);
        return map.TraverseFrom("AAA")
            .Select((s, i) => (Location: s, Step: i))
            .Where(tuple => tuple.Location == "ZZZ")
            .Select(tuple => tuple.Step)
            .First();
    }

    public object Part2(string input)
    {
        var map = Map.Parse(input);

        var steps = map.StartNodes.AsParallel()
            .Select(map.CycleLength);

        return steps.LeastCommonMultiple();
    }

    private static (string node, string left, string right) ParseInstruction(string line)
    {
        return (line[..3], line[7..10], line[12..15]);
    }

    public class Map
    {
        private readonly LinkedList<char> _directionList;
        private readonly IDictionary<string, (string Left, string Right)> _map;

        private Map(LinkedList<char> directionList, IDictionary<string, (string Left, string Right)> map, string[] startNodes)
        {
            _directionList = directionList;
            _map = map;
            StartNodes = startNodes;
        }

        public static Map Parse(string input)
        {
            var lines = input.ToLines().ToArray();
            var map = new Dictionary<string, (string Left, string Right)>();
            var directionList = new LinkedList<char>(lines[0]);
            foreach (var line in lines[2..])
            {
                (string node, string left, string right) = ParseInstruction(line);
                map.Add(node, (left, right));
            }

            var startNodes = map
                .Where(kvp => kvp.Key is [.., 'A'])
                .Select(kvp => kvp.Key)
                .ToArray();


            return new Map(directionList, map, startNodes);
        }

        public string[] StartNodes { get; }

        public IEnumerable<string> TraverseFrom(string start)
        {
            var direction = _directionList.First!;
            var node = start;
            while (true)
            {
                yield return node;
                (string left, string right) = _map[node];
                node = direction.Value switch
                {
                    'L' => left,
                    'R' => right,
                    _ => throw new InvalidOperationException()
                };
                direction = direction.NextOrFirst();
            }
        }

        public long CycleLength(string start)
        {
            using IEnumerator<string> enumerator = TraverseFrom(start).GetEnumerator();

            var set = new HashSet<(string, int)>();
            var current = (Node: string.Empty, Step: -1);
            var length = _directionList.Count;

            enumerator.MoveNext();

            do
            {
                current = (enumerator.Current, (current.Step + 1) % length);
            } while (set.Add(current) && enumerator.MoveNext());

            return set.Count - current.Step;
        }
    }
}
