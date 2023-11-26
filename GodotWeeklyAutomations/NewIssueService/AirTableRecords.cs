namespace NewIssueService;

public class AirTableRecords
{
    public IEnumerable<AirTableRecord> Records { get; set; } = new List<AirTableRecord>();
}

public class AirTableRecord
{
    public string Id { get; set; } = default!;
    public string CreatedTime { get; set; } = default!;
    public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
}

public static class AirTableRecordExtensions
{
    public static string Name(this AirTableRecord record) => record.Fields.GetKeyOrEmptyString("Name");
    public static string Status(this AirTableRecord record) => record.Fields.GetKeyOrEmptyString("Status");
    public static string Url(this AirTableRecord record) => record.Fields.GetKeyOrEmptyString("URL");
    public static string Source(this AirTableRecord record) => record.Fields.GetKeyOrEmptyString("Source");
    public static string Notes(this AirTableRecord record) => record.Fields.GetKeyOrEmptyString("Notes");
    public static string TargetCategory(this AirTableRecord record) => record.Fields.GetKeyOrEmptyString("TargetCategory");
    public static string NewUrl(this AirTableRecord record) => record.Fields.GetKeyOrEmptyString("NewURL");
    public static string NewDescription(this AirTableRecord record) => record.Fields.GetKeyOrEmptyString("NewDescription");

    private static string GetKeyOrEmptyString(this Dictionary<string, string> dict, string key) 
        => dict.ContainsKey(key) ? dict[key] : string.Empty;

    private static bool IsYouTubeLink(this AirTableRecord record)
    {
        var newUrl = record.NewUrl();

        if (string.IsNullOrWhiteSpace(newUrl))
        {
            newUrl = record.Url();
        }

        return newUrl.StartsWith("https://www.youtube.com") || newUrl.StartsWith("https://youtu.be");
    }

    private static string GetYouTubeThumbnail(this AirTableRecord record)
    {
        var youtubeUrl = record.GetUrl();
        var youtubeId = youtubeUrl.GetYoutubeClipId();

        return $"https://img.youtube.com/vi/{youtubeId}/mqdefault.jpg";
    }

    public static string GetTitle(this AirTableRecord record) 
        => !string.IsNullOrWhiteSpace(record.NewDescription()) ? record.NewDescription() : record.Name();

    public static string GetUrl(this AirTableRecord record)
        => !string.IsNullOrWhiteSpace(record.NewUrl()) ? record.NewUrl() : record.Url();

    public static string GetImage(this AirTableRecord record)
        => record.IsYouTubeLink() ? record.GetYouTubeThumbnail() : string.Empty;
}
