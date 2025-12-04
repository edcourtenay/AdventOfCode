using System.Numerics;

namespace AdventOfCode.Solutions;

public readonly record struct Direction<T>(T X, T Y) where T : ISignedNumber<T> {
    public static readonly Direction<T> North = new(T.Zero, T.NegativeOne);
    public static readonly Direction<T> South = new(T.Zero, T.One);
    public static readonly Direction<T> West = new(T.NegativeOne, T.Zero);
    public static readonly Direction<T> East = new(T.One, T.Zero);

    public static readonly Direction<T> NorthWest = new(T.NegativeOne, T.NegativeOne);
    public static readonly Direction<T> NorthEast = new(T.One, T.NegativeOne);
    public static readonly Direction<T> SouthWest = new(T.NegativeOne, T.One);
    public static readonly Direction<T> SouthEast = new(T.One, T.One);

    public static readonly Direction<T>[] Orthogonal = [North, South, West, East];
    public static readonly Direction<T>[] Cardinal = [North, South, West, East];
    public static readonly Direction<T>[] Ordinal = [NorthEast, SouthEast, SouthWest, NorthWest];
    public static readonly Direction<T>[] Compass = [..Cardinal, ..Ordinal];

    public Direction<T> RotateLeft => new(-Y, X);
    public Direction<T> RotateRight => new(Y, -X);

    public static implicit operator Direction<T>((T x, T y) d) => new(d.x, d.y);
}