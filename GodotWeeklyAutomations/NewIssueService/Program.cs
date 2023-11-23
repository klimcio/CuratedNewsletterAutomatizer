using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Text.Json;

namespace NewIssueService;

internal class Program
{
    static async Task Main(string[] args)
    {
        var configuration = GetConfiguration();

        if (configuration is null || 
            string.IsNullOrWhiteSpace(configuration?.AirtableApiGetTableContentEndpoint) || 
            string.IsNullOrWhiteSpace(configuration?.AirtableApiAccessToken))
        {
            Console.WriteLine("Configuration not set");
            return;
        }

        var records = (await configuration.AirtableApiGetTableContentEndpoint.GetContentAsync(
            configuration.AirtableApiAccessToken
        )).Parse();

        var nextIssueRecords = records?.Records.Where(x => AirTableRecordExtensions.Status(x) == "Next issue");

        Console.WriteLine("Hello, World!");
    }

    private static Configuration? GetConfiguration() =>
        new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json")
            .Build()
            .GetRequiredSection("Settings")
            .Get<Configuration>();
}

internal static class Extensions
{ 
    public static async Task<string> GetContentAsync(this string airTableEndpoint, string airTableToken)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", airTableToken);
        var rawRecords = await client.GetStringAsync(airTableEndpoint);

        return rawRecords;
    }

    public static AirTableRecords? Parse(this string rawRecords) 
        => JsonSerializer.Deserialize<AirTableRecords>(rawRecords, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
}
