namespace AdventOfCode.Solutions.Extensions;

public static class SequenceExtensions
{
    extension<T>(IEnumerable<T> source)
    {
        public IEnumerable<T> Flatten(Func<T, IEnumerable<T>> f)
        {
            var items = source as T[] ?? source.ToArray();
            return items.SelectMany(c => f(c).Flatten(f)).Concat(items);
        }

        public IEnumerable<T> TakeUntil(Func<T, bool> predicate)
        {
            foreach (var item in source)
            {
                yield return item;
                if (predicate(item))
                    yield break;
            }
        }
    }

    extension<T>(IEnumerable<IEnumerable<T>> source)
    {
        public IEnumerable<IEnumerable<T>> Pivot()
        {
            var enumerators = source.Select(e => e.GetEnumerator()).ToArray();
            try
            {
                while (enumerators.All(e => e.MoveNext()))
                    yield return enumerators.Select(e => e.Current).ToArray();
            }
            finally
            {
                Array.ForEach(enumerators, e => e.Dispose());
            }
        }
    }

    public static IEnumerable<T> ToEnumerable<T>(this T item)
    {
        yield return item;
    }
}

