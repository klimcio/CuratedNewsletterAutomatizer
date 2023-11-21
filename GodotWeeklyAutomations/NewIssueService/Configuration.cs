namespace NewIssueService;

internal class Configuration
{
    public string AirtableApiAccessToken { get; set; } = default!;
    public string AirtableApiGetTableContentEndpoint { get; set; } = default!;
}
