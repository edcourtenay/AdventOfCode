namespace AdventOfCode.Solutions.Year2025;

[Description("Movie Theater")]
public class Day09 : IPuzzle
{
    private record struct Point(long X, long Y);
    private record struct HorizontalEdge(long Y, long X1, long X2);
    private record struct VerticalEdge(long X, long Y1, long Y2);
    private record struct Rectangle(int VertexIndex1, int VertexIndex2, long Area);

    public object Part1(string input)
    {
        var vertices = ParseVertices(input);
        var rectangleAreas = CalculateRectangleAreas(vertices);
        return rectangleAreas.Dequeue().Area;
    }

    public object Part2(string input)
    {
        var vertices = ParseVertices(input);
        var rectangleAreas = CalculateRectangleAreas(vertices);
        (HorizontalEdge[] horizontalEdges, VerticalEdge[] verticalEdges) = BuildEdges(vertices);

        while (rectangleAreas.Count > 0)
        {
            var rect = rectangleAreas.Dequeue();
            var p1 = vertices[rect.VertexIndex1];
            var p2 = vertices[rect.VertexIndex2];
            
            if (IsRectangleInPolygon(p1.X, p1.Y, p2.X, p2.Y, horizontalEdges, verticalEdges))
                return rect.Area;
        }
        
        return 0L;
    }

    private static Point[] ParseVertices(string input)
    {
        return input.ToLines(line => line.Split(','))
            .Select(parts => new Point(long.Parse(parts[0]), long.Parse(parts[1])))
            .ToArray();
    }

    private static (HorizontalEdge[] horizontal, VerticalEdge[] vertical) BuildEdges(Point[] vertices)
    {
        var vertexCount = vertices.Length;
        var horizontalEdges = new List<HorizontalEdge>();
        var verticalEdges = new List<VerticalEdge>();

        for (int i = 0; i < vertexCount; i++)
        {
            var current = vertices[i];
            var next = vertices[(i + 1) % vertexCount];

            if (current.X == next.X)
            {
                // Vertical edge
                var (minY, maxY) = current.Y < next.Y ? (current.Y, next.Y) : (next.Y, current.Y);
                verticalEdges.Add(new VerticalEdge(current.X, minY, maxY));
            }
            else if (current.Y == next.Y)
            {
                // Horizontal edge
                var (minX, maxX) = current.X < next.X ? (current.X, next.X) : (next.X, current.X);
                horizontalEdges.Add(new HorizontalEdge(current.Y, minX, maxX));
            }
        }

        return (horizontalEdges.ToArray(), verticalEdges.ToArray());
    }

    private static bool IsPointOnEdge(Point point, HorizontalEdge[] horizontalEdges, VerticalEdge[] verticalEdges)
    {
        foreach (var edge in horizontalEdges)
        {
            if (edge.Y == point.Y && point.X >= edge.X1 && point.X <= edge.X2)
                return true;
        }

        foreach (var edge in verticalEdges)
        {
            if (edge.X == point.X && point.Y >= edge.Y1 && point.Y <= edge.Y2)
                return true;
        }

        return false;
    }

    private static bool IsPointInsidePolygon(Point point, HorizontalEdge[] horizontalEdges, VerticalEdge[] verticalEdges)
    {
        if (IsPointOnEdge(point, horizontalEdges, verticalEdges))
            return true;

        // Ray casting algorithm - count intersections with vertical edges
        int intersectionCount = 0;
        foreach (var edge in verticalEdges)
        {
            if ((edge.Y1 > point.Y) != (edge.Y2 > point.Y) && point.X < edge.X)
                intersectionCount++;
        }

        return intersectionCount % 2 == 1;
    }

    private static bool DoesHorizontalEdgeCrossInterior(long minY, long maxY, long x, HorizontalEdge[] horizontalEdges)
    {
        foreach (var edge in horizontalEdges)
        {
            if (edge.Y > minY && edge.Y < maxY && x > edge.X1 && x < edge.X2)
                return true;
        }
        return false;
    }

    private static bool DoesVerticalEdgeCrossInterior(long minX, long maxX, long y, VerticalEdge[] verticalEdges)
    {
        foreach (var edge in verticalEdges)
        {
            if (edge.X > minX && edge.X < maxX && y > edge.Y1 && y < edge.Y2)
                return true;
        }
        return false;
    }

    private static bool IsRectangleInPolygon(long x1, long y1, long x2, long y2, 
        HorizontalEdge[] horizontalEdges, VerticalEdge[] verticalEdges)
    {
        var (minX, maxX) = x1 < x2 ? (x1, x2) : (x2, x1);
        var (minY, maxY) = y1 < y2 ? (y1, y2) : (y2, y1);

        // Check all four corners are inside the polygon
        var corners = new[]
        {
            new Point(minX, minY),
            new Point(minX, maxY),
            new Point(maxX, minY),
            new Point(maxX, maxY)
        };

        foreach (var corner in corners)
        {
            if (!IsPointInsidePolygon(corner, horizontalEdges, verticalEdges))
                return false;
        }

        // Check no edges cross through the rectangle interior
        return !DoesVerticalEdgeCrossInterior(minX, maxX, minY, verticalEdges) &&
               !DoesVerticalEdgeCrossInterior(minX, maxX, maxY, verticalEdges) &&
               !DoesHorizontalEdgeCrossInterior(minY, maxY, minX, horizontalEdges) &&
               !DoesHorizontalEdgeCrossInterior(minY, maxY, maxX, horizontalEdges);
    }

    private static PriorityQueue<Rectangle, long> CalculateRectangleAreas(Point[] vertices)
    {
        var rectangleAreas = new PriorityQueue<Rectangle, long>();
        
        for (int i = 0; i < vertices.Length; i++)
        {
            for (int j = i + 1; j < vertices.Length; j++)
            {
                var area = CalculateRectangleArea(vertices[i], vertices[j]);
                rectangleAreas.Enqueue(new Rectangle(i, j, area), -area);
            }
        }
        
        return rectangleAreas;
    }

    private static long CalculateRectangleArea(Point p1, Point p2)
    {
        var width = Math.Abs(p2.X - p1.X) + 1;
        var height = Math.Abs(p2.Y - p1.Y) + 1;
        return width * height;
    }
}