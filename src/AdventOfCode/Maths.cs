using System.Numerics;

namespace AdventOfCode;

public static class Maths
{
    public static T LeastCommonMultiple<T>(this IEnumerable<T> numbers) where T : INumber<T>
    {
        return numbers.Aggregate(LeastCommonMultiple);
    }

    public static T LeastCommonMultiple<T>(T a, T b) where T : INumber<T>
    {
        return T.Abs(a * b) / GreatestCommonDivisor(a, b);
    }

    public static T GreatestCommonDivisor<T>(T a, T b) where T : INumber<T>
    {
        while (true)
        {
            if (b == T.Zero)
            {
                return a;
            }

            T temp = a;
            a = b;
            b = temp % b;
        }
    }
}