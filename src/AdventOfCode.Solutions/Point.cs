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
}

public readonly record struct Direction<T>(T X, T Y) where T : ISignedNumber<T> {
    public static readonly Direction<T> North = new(T.Zero, T.NegativeOne);
    public static readonly Direction<T> South = new(T.Zero, T.One);
    public static readonly Direction<T> West = new(T.NegativeOne, T.Zero);
    public static readonly Direction<T> East = new(T.One, T.Zero);

    public Direction<T> RotateLeft => new(-Y, X);
    public Direction<T> RotateRight => new(Y, -X);

    public static implicit operator Direction<T>((T x, T y) d) => new(d.x, d.y);
}