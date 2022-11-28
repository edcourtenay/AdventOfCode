namespace AdventOfCode2015CS;

public static class EnumerableExtensions
{
    public static IEnumerable<T[]> Permutations<T>(this T[] rgt) {
        IEnumerable<T[]> PermutationsRec(int i) {
            if (i == rgt.Length) {
                yield return rgt.ToArray();
            }

            for (var j = i; j < rgt.Length; j++) {
                (rgt[i], rgt[j]) = (rgt[j], rgt[i]);
                foreach (var perm in PermutationsRec(i + 1)) {
                    yield return perm;
                }
                (rgt[i], rgt[j]) = (rgt[j], rgt[i]);
            }
        }

        return PermutationsRec(0);
    }
}
