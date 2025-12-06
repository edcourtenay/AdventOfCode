using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;
using Microsoft.Extensions.Logging;

namespace AdventOfCode;

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
            LogInputFileFileAlreadyExists(fileInfo.PhysicalPath!);
            using StreamReader reader = new(fileInfo.CreateReadStream());
            return await reader.ReadToEndAsync(ct);
        }

        LogDownloadingInputFileFile(fileInfo.PhysicalPath!);
        string content = await _client.GetStringAsync($"/{year}/day/{day}/input", ct);
        await File.WriteAllTextAsync(fileInfo.PhysicalPath!, content, ct);
        return content;
    }

    [LoggerMessage(LogLevel.Debug, "Input file {File} already exists")]
    partial void LogInputFileFileAlreadyExists(string file);

    [LoggerMessage(LogLevel.Debug, "Downloading input file {File}")]
    partial void LogDownloadingInputFileFile(string file);
}