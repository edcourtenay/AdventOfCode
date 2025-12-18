using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.FileProviders.Physical;

namespace AdventOfCode.EnvFile;

public static class EnvConfigurationExtensions
{
    extension(IConfigurationBuilder builder)
    {
        public IConfigurationBuilder AddEnvFile(string path = ".env",
            bool optional = false,
            bool reloadOnChange = false)
        {
            var fileProvider = new PhysicalFileProvider(AppContext.BaseDirectory, ExclusionFilters.System);
            return AddEnvFile(builder, path: path, optional: optional, reloadOnChange: reloadOnChange, provider: fileProvider);
        }

        public IConfigurationBuilder AddEnvFile(IFileProvider provider,
            string path,
            bool optional,
            bool reloadOnChange)
        {
            ArgumentNullException.ThrowIfNull(builder);
            ArgumentException.ThrowIfNullOrEmpty(path);

            return builder.AddEnvFile(s =>
            {
                s.FileProvider = provider;
                s.Path = path;
                s.Optional = optional;
                s.ReloadOnChange = reloadOnChange;
                s.ResolveFileProvider();
            });
        }

        public IConfigurationBuilder AddEnvFile(Action<EnvConfigurationSource> configureSource)
        {
            return builder.Add(configureSource);
        }
    }
}