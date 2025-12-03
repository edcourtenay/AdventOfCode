using System.Numerics;

namespace AdventOfCode.Solutions.Extensions;

public static class RangeExtensions
{
    extension<T>((T from, T to) range) where T : IBinaryInteger<T>
    {
        public bool Contains(T value)
        {
            return range.from <= value && value <= range.to;
        }

        public bool Intersects((T from, T to) other)
        {
            return range.from <= other.to && other.from <= range.to;
        }

        public IEnumerable<(T from, T to)> ExcludeRange((T from, T to) exclude)
        {
            return range.ToEnumerable().ExcludeRanges(exclude.ToEnumerable());
        }

        public T RangeLength()
        {
            return range.to - range.from + T.One;
        }
    }

    extension<T>(IEnumerable<(T from, T to)> source) where T : IBinaryInteger<T>
    {
        public IEnumerable<(T from, T to)> ExcludeRange((T from, T to) exclude)
        {
            return source.ExcludeRanges(exclude.ToEnumerable());
        }

        public IEnumerable<(T from, T to)> ExcludeRanges(IEnumerable<(T from, T to)> exclude)
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

        public IEnumerable<(T from, T to)> MergeOverlapping()
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
    }
}