namespace AdventOfCode.Year2023;

[Description("Not set...")]
public class Day24 : IPuzzle
{
    private const double Tolerance = 0.00001;

    public object Part1(string input)
    {
        return SolvePart1(input, (200000000000000, 400000000000000));
    }

    public object Part2(string input)
    {
        return SolvePart2(input, (-500, 500));
    }

    public static long SolvePart2(string input, (long from, long to) range)
    {
        var hailstones = input.ToLines(Hailstone.Parse).ToList();

        while (true)
        {
            var hail = hailstones.OrderBy(_ => Random.Shared.Next()).Take(4).ToArray();

            for (long deltaX = range.from; deltaX <= range.to; deltaX++)
            {
                for (long deltaY = range.from; deltaY <= range.to; deltaY++)
                {
                    var hail0 = hail[0].WithVelocityDelta(deltaX, deltaY);
                    var x = deltaX;
                    var y = deltaY;
                    var intercepts = hail.Skip(1)
                        .Select(h => h.WithVelocityDelta(x, y).IntersectionWith(hail0))
                        .Where(i => i.HasValue)
                        .Cast<Intersection>()
                        .ToArray();

                    if (intercepts.Length != 3 || !intercepts.Skip(1).All(i =>
                            Math.Abs(i.X - intercepts[0].X) < Tolerance && Math.Abs(i.Y - intercepts[0].Y) < Tolerance))
                    {
                        continue;
                    }

                    for (long deltaZ = range.from; deltaZ <= range.to; deltaZ++)
                    {
                        var z1 = hail[1].PredictZ(intercepts[0].Time, deltaZ);
                        var z2 = hail[2].PredictZ(intercepts[1].Time, deltaZ);
                        var z3 = hail[3].PredictZ(intercepts[2].Time, deltaZ);

                        if (Math.Abs(z1 - z2) < Tolerance && Math.Abs(z2 - z3) < Tolerance)
                        {
                            return (long)(intercepts[0].X + intercepts[0].Y + z1);
                        }
                    }
                }
            }
        }
    }

    public static int SolvePart1(string input, (double from, double to) range)
    {
        var hailstones = input.ToLines(Hailstone.Parse).ToList();

        return hailstones
            .CartesianPairs()
            .Where(t => t.left != t.right)
            .Select(t => t.left.IntersectionWith(t.right))
            .Where(i => i.HasValue)
            .Cast<Intersection>()
            .Count(i => range.Contains(i.X) && range.Contains(i.Y));
    }

    public readonly record struct Hailstone
    {
        public Point3D Position { get; init; }
        public Vector3D Velocity { get; init; }

        private Lazy<double> LazySlope
        {
            get
            {
                Hailstone hailstone = this;
                return new Lazy<double>(() => hailstone.Velocity.X == 0L ? double.NaN : hailstone.Velocity.Y / (double)hailstone.Velocity.X);
            }
        }

        private double Slope => LazySlope.Value;

        public static Hailstone Parse(string input)
        {
            var parts = input.Split('@');
            return new Hailstone
            {
                Position = Point3D.Parse(parts[0]),
                Velocity = Vector3D.Parse(parts[1])
            };
        }

        public Intersection? IntersectionWith(Hailstone other)
        {
            if (double.IsNaN(Slope) || double.IsNaN(other.Slope) || Slope == other.Slope)
            {
                return null;
            }

            var c = Position.Y - Slope * Position.X;
            var otherC = other.Position.Y - other.Slope * other.Position.X;

            var x = (otherC - c) / (Slope - other.Slope);
            var t1 = (x - Position.X) / Velocity.X;
            var t2 = (x - other.Position.X) / other.Velocity.X;

            if (t1 < 0 || t2 < 0)
            {
                return null;
            }

            var y = Slope * (x - Position.X) + Position.Y;
            return new Intersection(x, y, t1);
        }

        public Hailstone WithVelocityDelta(long vx, long vy) =>
            this with { Velocity = new Vector3D(Velocity.X + vx, Velocity.Y + vy, Velocity.Z) };

        public double PredictZ(double time, long zDelta) => Position.Z + time * (Velocity.Z + zDelta);
    }

    public readonly record struct Point3D(long X, long Y, long Z)
    {
        public static Point3D Parse(string input)
        {
            var parts = input.Split(',');
            return new Point3D(
                long.Parse(parts[0]),
                long.Parse(parts[1]),
                long.Parse(parts[2])
            );
        }
    }

    public readonly record struct Vector3D(long X, long Y, int Z)
    {
        public static Vector3D Parse(string input)
        {
            var parts = input.Split(',');
            return new Vector3D(
                int.Parse(parts[0]),
                int.Parse(parts[1]),
                int.Parse(parts[2])
            );
        }
    }

    public readonly struct Intersection(double X, double Y, double Time)
    {
        public double X { get; } = X;
        public double Y { get; } = Y;
        public double Time { get; } = Time;
    }
}