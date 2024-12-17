using System.Numerics;

namespace AdventOfCode.Solutions;

public readonly record struct Point<T>(T X, T Y) where T : ISignedNumber<T>
{
    public void Deconstruct(out T x, out T y)
    {
        x = X;
        y = Y;
    }

    public static Direction<T> operator -(Point<T> p1, Point<T> p2) => new(p1.X - p2.X, p1.Y - p2.Y);
    public static Direction<T> operator +(Point<T> p1, Point<T> p2) => new(p1.X + p2.X, p1.Y + p2.Y);

    public static Point<T> operator +(Point<T> p, Direction<T> d) => new(p.X + d.X, p.Y + d.Y);
    public static Point<T> operator -(Point<T> p, Direction<T> d) => new(p.X - d.X, p.Y - d.Y);

    public T ManhattanDistance(Point<T> other) =>
        ManhattanDistance(this, other);

    public static T ManhattanDistance(Point<T> p1, Point<T> p2) =>
        T.Abs(p1.X - p2.X) + T.Abs(p1.Y - p2.Y);

    public static implicit operator Point<T>((T x, T y) p) => new(p.x, p.y);

    public static Point<T> Zero => new(T.Zero, T.Zero);
}