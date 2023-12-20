using System.Numerics;

namespace AdventOfCode;

public static class RangeExtensions
{
    public static bool Contains<T>(this (T from, T to) range, T value) where T : IBinaryInteger<T>
    {
        return range.from <= value && value <= range.to;
    }

    public static IEnumerable<(T from, T to)> MergeOverlapping<T>(this IEnumerable<(T from, T to)> source)  where T : IBinaryInteger<T>
    {
        source = source.OrderBy(r => r.from);

        (T from, T to) current = default;
        bool first = true;
        foreach (var item in source)
        {
            if (first)
            {
                current = item;
                first = false;
            }
            else
            {
                if (current.Contains(item.from))
                {
                    current = (current.from, item.to)!;
                }
                else
                {
                    yield return current;
                    current = item;
                }
            }
        }
        if (!first) yield return current;
    }

    public static IEnumerable<(T from, T to)> ExcludeRange<T>(this (T from, T to) range,
        (T from, T to) exclude)  where T : IBinaryInteger<T>
    {
        return range.ToEnumerable().ExcludeRanges(exclude.ToEnumerable());
    }

    public static IEnumerable<(T from, T to)> ExcludeRange<T>(this IEnumerable<(T from, T to)> source,
        (T from, T to) exclude) where T : IBinaryInteger<T>
    {
        return source.ExcludeRanges(exclude.ToEnumerable());
    }

    public static IEnumerable<(T from, T to)> ExcludeRanges<T>(this IEnumerable<(T from, T to)> source,
        IEnumerable<(T from, T to)> exclude) where T : IBinaryInteger<T>
    {
        source = source.OrderBy(r => r.from);
        exclude = exclude.OrderBy(r => r.from);

        Queue<(T from, T to)> queue = new(exclude);

        foreach ((T from, T to) r in source)
        {
            (T from, T to)? current = r;

            while (queue.Count > 0 && current != null)
            {
                (T from, T to) ex = queue.Peek();

                if (ex.to < current.Value.from)
                {
                    queue.Dequeue();
                    continue;
                }

                if (ex.from > current.Value.to)
                {
                    break;
                }

                if (ex.from <= current.Value.from && ex.to >= current.Value.to)
                {
                    current = null;
                    continue;
                }

                if (ex.from <= current.Value.from && ex.to < current.Value.to)
                {
                    current = (ex.to + T.One, current.Value.to);
                    queue.Dequeue();
                    continue;
                }

                if (ex.from > current.Value.from && ex.to >= current.Value.to)
                {
                    current = (current.Value.from, ex.from - T.One);
                    continue;
                }

                if (ex.from > current.Value.from && ex.to < current.Value.to)
                {
                    yield return (current.Value.from, ex.from - T.One);

                    current = (ex.to + T.One, current.Value.to);
                    queue.Dequeue();
                }
            }

            if (current != null)
            {
                yield return current.Value;
            }
        }
    }

    public static T RangeLength<T>(this (T from, T to) range)  where T : IBinaryInteger<T>
    {
        return range.to - range.from + T.One;
    }
}