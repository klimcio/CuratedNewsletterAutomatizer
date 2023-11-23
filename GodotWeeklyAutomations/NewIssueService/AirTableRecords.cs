namespace NewIssueService;

internal class AirTableRecords
{
    public IEnumerable<AirTableRecord> Records { get; set; } = new List<AirTableRecord>();
}

internal class AirTableRecord
{
    public string Id { get; set; } = default!;
    public string CreatedTime { get; set; }
    public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
}

internal static class AirTableRecordExtensions
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
}