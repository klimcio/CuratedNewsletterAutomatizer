using System.Net.Http.Headers;
using System.Text.Json;
using System.Web;

namespace NewIssueService;

public static class Extensions
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

    public static string GetYoutubeClipId(this string youtubeUrl)
    {
        if (string.IsNullOrWhiteSpace(youtubeUrl))
            return string.Empty;

        var uri = new Uri(youtubeUrl);

        switch(uri.Host)
        {
            case "www.youtube.com":
                return HttpUtility.ParseQueryString(uri.Query)["v"] ?? string.Empty;
            case "youtu.be":
                return uri.Segments[1];
            default:
                return string.Empty;
        }
    }
}
