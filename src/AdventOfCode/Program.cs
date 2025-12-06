using AdventOfCode;
using AdventOfCode.Configuration;
using AdventOfCode.EnvFile;

using Cocona;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

var builder = CoconaApp.CreateBuilder();

builder.Host
    // .ConfigureLogging(logging =>
    // {
    //     logging.ClearProviders();
    //     logging.AddJsonConsole(options =>
    //     {
    //         options.IncludeScopes = true;
    //     });
    //     logging.AddDebug();
    // })
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile("appsettings.json", optional: true);
        config.AddEnvFile(optional: true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<InputDownloader>((provider, client) =>
        {
            client.BaseAddress = new Uri("https://adventofcode.com");
            var settings = provider.GetRequiredService<IOptions<ApplicationSettings>>();
            client.DefaultRequestHeaders.Add("Cookie", $"session={settings.Value.SessionId}");
        });

        services.TryAddEnumerable(ServiceDescriptor.Singleton<IValidateOptions<ApplicationSettings>, ApplicationSettings>());

        services.AddOptions<ApplicationSettings>()
            .Bind(context.Configuration)
            .ValidateOnStart();

        services.AddSingleton<Application>();
    });

var app = builder.Build();

await app.RunAsync<Application>();
