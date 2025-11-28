namespace AdventOfCode.EnvFile;

internal static class EnvReader
{
    public static IEnumerable<KeyValuePair<string, string>> Load(Stream stream)
    {
        using StreamReader reader = new(stream);

        while (reader.Peek() > -1)
        {
            var line = reader.ReadLine();

            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#'))
            {
                continue;
            }

            var split = line.Split('=', 2);
            if (split is [var key, var value])
            {
                yield return new KeyValuePair<string, string>(key.Trim(), value.Trim());
            }
        }
    }
}