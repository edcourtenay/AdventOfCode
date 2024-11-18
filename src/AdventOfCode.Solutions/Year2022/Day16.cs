using System.Text.RegularExpressions;

namespace AdventOfCode.Solutions.Year2022;

[Description("Proboscidea Volcanium")]
public partial class Day16 : IPuzzle
{
    public object Part1(string input)
    {
        return Solve(input, (flows, paths, bitMasks) =>
            Visit("AA", 30, 0, 0, flows, paths, bitMasks).Values.Max());
    }

    public object Part2(string input)
    {
        return Solve(input, (flows, paths, bitMasks) =>
        {
            Dictionary<int, long> visited = Visit("AA", 26, 0, 0, flows, paths, bitMasks);

            return visited.Keys.SelectMany(k1 => visited.Keys.Select(k2 => (k1, k2)))
                .Where(t => (t.k1 & t.k2) == 0)
                .Select(t => visited[t.k1] + visited[t.k2]).Max();
        });
    }

    private static long Solve(string input, Func<Dictionary<string, int>, Dictionary<string, Dictionary<string, long>>, Dictionary<string, int>, long> func)
    {
        var lines = Enumerable
            .Select<string, string[]>(input.ToLines(), x => LineSplitRegex().Split(x)).ToList();

        var neighbours = lines
            .ToDictionary(x => x[1], x => new HashSet<string>(x.Skip(10)));

        var flows = lines
            .Where(x => int.Parse(x[5]) != 0)
            .ToDictionary(x => x[1], x => int.Parse(x[5]));

        var bitMasks = flows.Keys
            .Select((x, i) => (x, i))
            .ToDictionary(t => t.x, t => 1 << t.i);

        var paths = neighbours.Keys
            .ToDictionary(x => x, x => neighbours.Keys.ToDictionary(y => y,
            y => neighbours[x].Contains(y) ? 1 : (long)int.MaxValue));

        foreach (string k in paths.Keys)
        {
            foreach (string i in paths.Keys)
            {
                foreach (string j in paths.Keys)
                {
                    paths[i][j] = Math.Min(paths[i][j], paths[i][k] + paths[k][j]);
                }
            }
        }

        return func(flows, paths, bitMasks);
    }

    private static Dictionary<int, long> Visit(string start, long budget, int state, long flow,
        Dictionary<string, int> flows, Dictionary<string, Dictionary<string, long>> paths, Dictionary<string, int> bitMasks)
    {
        return Visit(start, budget, state, flow, flows, paths, bitMasks, new Dictionary<int, long>());
    }

    private static Dictionary<int, long> Visit(string start, long budget, int state, long flow,
        Dictionary<string, int> flows, Dictionary<string, Dictionary<string, long>> paths, Dictionary<string, int> bitMasks,
        Dictionary<int, long> answer)
    {
        answer[state] = Math.Max(answer.GetValueOrDefault(state, 0), flow);

        foreach (string key in flows.Keys)
        {
            long newBudget = budget - paths[start][key] - 1;

            if ((bitMasks[key] & state) != 0 || newBudget <= 0)
            {
                continue;
            }

            Visit(key, newBudget, state | bitMasks[key], flow + (newBudget * flows[key]), flows, paths, bitMasks, answer);
        }

        return answer;
    }

    [GeneratedRegex(@"[\s=;,]+")]
    private static partial Regex LineSplitRegex();
}