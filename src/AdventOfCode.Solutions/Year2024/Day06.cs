using Direction = (int dx, int dy);
using Point = (int x, int y);
namespace AdventOfCode.Solutions.Year2024;

[Description("Guard Gallivant")]
public class Day06 : IPuzzle
{
    public object Part1(string input)
    {
        (Point pos, HashSet<Point> obstacles, int width, int height) = Parse(input);
        Direction dir = (0, -1);
        
        HashSet<Point> visited = [];
        while (pos is { x: >= 0, y: >= 0 } && pos.x < width && pos.y < height)
        {
            Point lookAt;
            
            visited.Add(pos);
            while (obstacles.Contains(lookAt = (pos.x + dir.dx, pos.y + dir.dy)))
            {
                dir = RotateRight(dir);
            }
            
            pos = lookAt;
        }
        
        return visited.Count;
    }

    public object Part2(string input)
    {
        return string.Empty;
    }

    private (Point start, HashSet<Point> obstacles, int width, int height) Parse(string input)
    {
        var set = new HashSet<Point>();
        Point start = (0, 0);
        int maxX = 0;
        int maxY = 0;
        foreach ((int y, string item) in input.ToLines().Index())
        {
            foreach ((int x, char c) in item.Index())
            {
                switch (c)
                {
                    case '#':
                        set.Add((x, y));
                        break;
                    case '^':
                        start = (x, y);
                        break;
                }
                maxX = int.Max(x, maxX);
            }
            maxY = int.Max(y, maxY);
        }
        
        return (start, set, maxX + 1, maxY + 1);
    }
    
    private Direction RotateRight(Direction dir) =>
        (dir.dy * -1, dir.dx);
}
