namespace AdventOfCode.Solutions.Year2025;

[Description("Laboratories")]
public class Day07 : IPuzzle
{
    public object Part1(string input)
    {
        int splits = 0;
        HashSet<int> beams = [];
        foreach ((int index, string line) in input.ToLines().Index())
        {
            if (index == 0)
            {
                beams.Add(line.IndexOf('S'));
                continue;
            }

            if (index % 2 == 1)
            {
                continue;
            }

            foreach (var beam in beams.ToList().Where(beam => line[beam] == '^'))
            {
                splits++;
                beams.Remove(beam);
                beams.Add(beam - 1);
                beams.Add(beam + 1);
            }
        }

        return splits;
    }

    public object Part2(string input)
    {
        Dictionary<int, long> dict = [];
        foreach ((int index, string line) in input.ToLines().Index())
        {
            if (index == 0)
            {
                dict.Add(line.IndexOf('S'), 1);
                continue;
            }

            if (index % 2 == 1)
            {
                continue;
            }

            Dictionary<int, long> newDict = [];
            foreach (var beam in dict.Keys.ToList())
            {
                if (line[beam] == '^')
                {
                    var current = dict[beam];
                    SetOrIncrement(newDict, beam - 1, current);
                    SetOrIncrement(newDict, beam + 1, current);
                }
                else
                {
                    SetOrIncrement(newDict, beam, dict[beam]);
                }
            }
            dict = newDict;
        }

        return dict.Values.Sum();
    }

    private void SetOrIncrement(Dictionary<int, long> dict, int beam, long current)
    {
        if (dict.TryGetValue(beam, out var value))
        {
            dict[beam] = value + current;
        }
        else
        {
            dict[beam] = current;
        }
    }
}