using System.Web;

namespace NewIssueService;

public static class Extensions
{
    public static string GetYoutubeClipId(this string youtubeUrl)
    {
        if (string.IsNullOrWhiteSpace(youtubeUrl))
            return string.Empty;

        var uri = new Uri(youtubeUrl);

        switch (uri.Host)
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
