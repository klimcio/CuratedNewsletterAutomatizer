using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace NewIssueService;

public static class AirTableApiExtensions
{
    private static string ComposeAirTableApiUrl(this string airTableEndpoint, string? viewId)
    {
        StringBuilder builder = new(airTableEndpoint);

        if (!string.IsNullOrWhiteSpace(viewId))
            builder.Append($"?view={viewId}");

        return builder.ToString();
    }

    public static async Task<string> GetContentAsync(this string airTableEndpoint, string airTableToken, string? viewId)
    {
        using HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", airTableToken);
        var rawRecords = await client.GetStringAsync(airTableEndpoint.ComposeAirTableApiUrl(viewId));

        return rawRecords;
    }

    public static AirTableRecords? Parse(this string rawRecords)
        => JsonSerializer.Deserialize<AirTableRecords>(rawRecords, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
}
