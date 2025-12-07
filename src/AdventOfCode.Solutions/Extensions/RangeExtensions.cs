using System.Numerics;
using System.Runtime.CompilerServices;

namespace AdventOfCode.Solutions.Extensions;

public static class RangeExtensions
{
    extension<T>((T from, T to) range) where T : IBinaryInteger<T>
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T value)
        {
            return range.from <= value && value <= range.to;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Intersects((T from, T to) other)
        {
            return range.from <= other.to && other.from <= range.to;
        }

        public IEnumerable<(T from, T to)> ExcludeRange((T from, T to) exclude)
        {
            // Fast path without allocations: subtract a single exclude from a single range
            var (rf, rt) = range;
            var (ef, et) = exclude;

            // No intersection
            if (et < rf || ef > rt)
            {
                yield return (rf, rt);
                yield break;
            }

            // Exclude fully covers the range -> nothing remains
            if (ef <= rf && et >= rt)
            {
                yield break;
            }

            // Left piece
            if (ef > rf)
            {
                var leftTo = T.Min((ef - T.One), rt);
                if (rf <= leftTo)
                    yield return (rf, leftTo);
            }

            // Right piece
            if (et < rt)
            {
                var rightFrom = T.Max((et + T.One), rf);
                if (rightFrom <= rt)
                    yield return (rightFrom, rt);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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
            // Materialize and sort inputs once to avoid repeated enumerations and allocations
            var srcArr = source as (T from, T to)[] ?? source.ToArray();
            var exArr = exclude as (T from, T to)[] ?? exclude.ToArray();

            if (srcArr.Length == 0)
                yield break;

            if (exArr.Length == 0)
            {
                for (int k = 0; k < srcArr.Length; k++)
                    yield return srcArr[k];
                yield break;
            }

            Array.Sort(srcArr, static (a, b) => a.from.CompareTo(b.from));
            Array.Sort(exArr, static (a, b) => a.from.CompareTo(b.from));

            int j = 0; // index into exArr
            for (int i = 0; i < srcArr.Length; i++)
            {
                var (sf, st) = srcArr[i];
                var start = sf;
                var end = st;

                // Skip all excludes that end before current start
                while (j < exArr.Length && exArr[j].to < start)
                    j++;

                int jj = j;
                var covered = false;
                while (jj < exArr.Length && exArr[jj].from <= end)
                {
                    var (ef, et) = exArr[jj];

                    // Left piece before this exclude
                    if (ef > start)
                    {
                        var leftTo = T.Min(ef - T.One, end);
                        if (start <= leftTo)
                            yield return (start, leftTo);
                    }

                    // If this exclude reaches or passes the end, the remainder is fully covered
                    if (et >= end)
                    {
                        covered = true;
                        break;
                    }

                    // Move start to the right of the exclude (safe: et < end ensures no overflow on +1)
                    var s = et + T.One;
                    if (s > start)
                        start = s;

                    if (start > end)
                        break;

                    jj++;
                }

                if (!covered && start <= end)
                    yield return (start, end);
            }
        }

        public IEnumerable<(T from, T to)> Merge()
        {
            var arr = source as (T from, T to)[] ?? source.ToArray();
            if (arr.Length == 0)
                yield break;

            Array.Sort(arr, static (a, b) => a.from.CompareTo(b.from));

            var curFrom = arr[0].from;
            var curTo = arr[0].to;

            for (int i = 1; i < arr.Length; i++)
            {
                var r = arr[i];
                // Overlap or adjacency; compute adjacency safely without overflow
                var overlaps = r.from <= curTo;
                var next = curTo + T.One;
                var adjacent = next > curTo && r.from == next;
                if (overlaps || adjacent)
                {
                    if (r.to > curTo)
                        curTo = r.to;
                }
                else
                {
                    yield return (curFrom, curTo);
                    curFrom = r.from;
                    curTo = r.to;
                }
            }

            yield return (curFrom, curTo);
        }
    }
}