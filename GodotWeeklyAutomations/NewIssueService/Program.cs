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
            configuration.AirtableApiAccessToken, configuration.AirTableFetchFromView
        )).Parse();

        Console.WriteLine($"Records downloaded: {records?.Records.Count()}");

        var nextIssueRecords = records?.Records
            .Where(x => AirTableRecordExtensions.Status(x) == "Next issue")
            .Where(x => !string.IsNullOrWhiteSpace(x.TargetCategory()))
            .Take(49)
            .Select(x => CuratedLink.Create(x))
            .ToList();

        Console.WriteLine($"Records found: {nextIssueRecords.Count()}");

        foreach(var link in nextIssueRecords!)
        {
            await configuration.CuratedApiEndpoint
                .ForPublication(configuration.CuratedPublicationId)
                .AddLinkToNextIssueAsync(configuration.CuratedApiToken, link);
        }

        Console.WriteLine("Hello, World!");
    }

    private static Configuration? GetConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json")
            .Build()
            .GetRequiredSection("Settings")
            .Get<Configuration>();
}
