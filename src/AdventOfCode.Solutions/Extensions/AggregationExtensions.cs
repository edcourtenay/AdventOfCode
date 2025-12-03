namespace AdventOfCode.Solutions.Extensions;

public static class AggregationExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public (T Min, T Max) MinMax(IComparer<T> comparer)
        {
            using var enumerator = source.GetEnumerator();

            if (!enumerator.MoveNext())
                throw new InvalidOperationException("Sequence contains no elements.");

            var min = enumerator.Current;
            var max = enumerator.Current;

            while (enumerator.MoveNext())
            {
                var current = enumerator.Current;
                if (comparer.Compare(current, min) < 0)
                    min = current;
                if (comparer.Compare(current, max) > 0)
                    max = current;
            }

            return (min, max);
        }

        public (TValue Min, TValue Max) MinMaxBy<TValue>(Func<T, TValue> selector) where TValue : IComparable<TValue> =>
            MinMaxBy(source, selector, Comparer<TValue>.Default);
    }

    extension<T>(IEnumerable<T> source) where T : IComparable<T>
    {
        public (T Min, T Max) MinMax() => MinMax(source, Comparer<T>.Default);
    }

    private static (TValue Min, TValue Max) MinMaxBy<TSource, TValue>(
        IEnumerable<TSource> source,
        Func<TSource, TValue> selector,
        Comparer<TValue> comparer) where TValue : IComparable<TValue>
    {
        using var enumerator = source.GetEnumerator();

        if (!enumerator.MoveNext())
            throw new InvalidOperationException("Sequence contains no elements.");

        var min = selector(enumerator.Current);
        var max = min;

        while (enumerator.MoveNext())
        {
            var value = selector(enumerator.Current);

            if (comparer.Compare(value, min) < 0)
                min = value;
            if (comparer.Compare(value, max) > 0)
                max = value;
        }

        return (min, max);
    }
}

