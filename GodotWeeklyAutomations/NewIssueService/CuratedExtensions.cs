using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace NewIssueService;

public static class CuratedExtensions
{
    public static string ForPublication(this string CuratedApiEndpoint, int publicationId)
        => $"{CuratedApiEndpoint}/{publicationId}";

    private static string ComposePostUrl(this string CuratedApiEndpoint, CuratedLink link)
    {
        StringBuilder builder = new($"{CuratedApiEndpoint}/links?");

        builder.Append($"url={WebUtility.UrlEncode(link.Url)}");
        builder.Append($"&title={WebUtility.UrlEncode(link.Title)}");
        builder.Append($"&category={WebUtility.UrlEncode(link.Category.ToLower())}");

        if (!string.IsNullOrWhiteSpace(link.Image))
        {
            builder.Append($"&image={WebUtility.UrlEncode(link.Image)}");
        }

        return builder.ToString();
    }

    public static async Task<bool> AddLinkToNextIssueAsync(this string CuratedApiEndpoint, string token, CuratedLink link)
    {
        var url = CuratedApiEndpoint.ComposePostUrl(link);

        using HttpClient client = new();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", $"token=\"{token}\"");
        var rawRecords = await client.PostAsync(url, null);

        return rawRecords.IsSuccessStatusCode;
    }
}
