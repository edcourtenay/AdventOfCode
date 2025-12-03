namespace AdventOfCode.Solutions.Extensions;

public static class ArrayExtensions
{
    extension<T>(T[] source)
    {
        public IEnumerable<T[]> Permutations()
        {
            return GeneratePermutations(source, 0);
        }
    }

    private static IEnumerable<T[]> GeneratePermutations<T>(T[] array, int i)
    {
        if (i == array.Length)
        {
            yield return array.ToArray();
            yield break;
        }

        for (var j = i; j < array.Length; j++)
        {
            (array[i], array[j]) = (array[j], array[i]);

            foreach (var perm in GeneratePermutations(array, i + 1))
                yield return perm;

            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}

