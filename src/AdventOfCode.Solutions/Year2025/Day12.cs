namespace AdventOfCode.Solutions.Year2025;

[Description("Christmas Tree Farm")]
public class Day12 : IPuzzle
{
    public object Part1(string input)
    {
        var data = input.ToLines().ToSequences(string.IsNullOrWhiteSpace).ToArray();
        var regionData = data[^1];
        
        Dictionary<int, int> areas = ParsePresents(data);
        List<Region> regions = ParseRegions(regionData);
        
        return regions
            .Count(region =>
            {
                return region.Area >= region.PresentCounts.Select(kvp => kvp.Value * areas[kvp.Key]).Sum();
            });
    }

    private static Dictionary<int, int> ParsePresents(IEnumerable<string>[] data)
    {
        var areas = new Dictionary<int, int>();

        foreach (var shapeData in data[..^1])
        {
            int shapeIndex = 0;
            int area = 0;
            foreach (string line in shapeData)
            {
                if (line is [.. var s, ':'])
                {
                    shapeIndex = int.Parse(s);
                    continue;
                }
                
                area += line.Count(c => c == '#');
            }
            areas[shapeIndex] = area;
        }

        return areas;
    }

    private static List<Region> ParseRegions(IEnumerable<string> regionData)
    {
        List<Region> regions = [];
        foreach (var line in regionData)
        {
            int area = 0;
            var splits = line.Split(' ');
            if (splits[0].Split('x') is [var w, var h])
            {
                area = int.Parse(w) * int.Parse(h[..^1]);
            }
            
            regions.Add(new Region
            {
                Area = area,
                PresentCounts = splits[1..].Index()
                    .ToDictionary(x => x.Index, x => int.Parse(x.Item))
            });
        }

        return regions;
    }

    public object Part2(string input)
    {
        throw new NotImplementedException();
    }

    private record Region
    {
         public required int Area { get; init; }
         public required Dictionary<int, int> PresentCounts { get; init; }
    }
}