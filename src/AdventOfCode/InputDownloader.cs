using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Logging;

public partial class InputDownloader
{
    private readonly HttpClient _client;
    private readonly ILogger<InputDownloader> _logger;

    public InputDownloader(HttpClient client, ILogger<InputDownloader> logger)
    {
        _client = client;
        _logger = logger;
    }

    public async Task<string> ReadOrDownload(int year, int day, CancellationToken ct = default)
    {
        PhysicalFileProvider provider = new(AppContext.BaseDirectory,
            ExclusionFilters.Hidden | ExclusionFilters.System);

        string yearFolder = Path.Combine("input", $"{year:0000}");
        string inputFile = Path.Combine(yearFolder, $"{day:00}.txt");

        Directory.CreateDirectory(Path.Combine(provider.Root, yearFolder));

        IFileInfo fileInfo = provider.GetFileInfo(inputFile);
        if (fileInfo.Exists)
        {
            _logger.LogInformation("Input file {File} already exists", fileInfo.PhysicalPath);
            using StreamReader reader = new(fileInfo.CreateReadStream());
            return await reader.ReadToEndAsync(ct);
        }

        _logger.LogInformation("Downloading input file {File}", fileInfo.PhysicalPath);
        string content = await _client.GetStringAsync($"/{year}/day/{day}/input", ct);
        await File.WriteAllTextAsync(fileInfo.PhysicalPath!, content, ct);
        return content;
    }
}