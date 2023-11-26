using Microsoft.Extensions.Configuration;

namespace NewIssueService;

internal class Program
{
    static async Task Main(string[] args)
    {
        var configuration = GetConfiguration();

        if (configuration is null || !configuration.IsValid())
        {
            Console.WriteLine("Configuration not set");
            return;
        }

        var records = (await configuration.AirtableApiGetTableContentEndpoint.GetContentAsync(
            configuration.AirtableApiAccessToken
        )).Parse();

        var nextIssueRecords = records?.Records
            .Where(x => AirTableRecordExtensions.Status(x) == "Next issue")
            .Where(x => !string.IsNullOrWhiteSpace(x.TargetCategory()))
            .Select(x => CuratedLink.Create(x))
            .ToList()
            .ForEach(x =>
            {

            });

        Console.WriteLine("Hello, World!");
    }

    private static Configuration? GetConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json")
            .Build()
            .GetRequiredSection("Settings")
            .Get<Configuration>();
}
