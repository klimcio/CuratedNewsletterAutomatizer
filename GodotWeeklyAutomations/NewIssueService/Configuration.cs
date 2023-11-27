namespace NewIssueService;

internal class Configuration
{
    public string AirtableApiAccessToken { get; set; } = default!;
    public string AirtableBaseId { get; set; } = default!;
    public string AirtableTableName { get; set; } = default!;
    public string AirtableApiUrl { get; set; } = default!;
    public string? AirTableFetchFromView { get; set; }

    public string CuratedApiToken { get; set; } = default!;
    public string CuratedApiEndpoint { get; set; } = default!;
    public int CuratedPublicationId { get; set; } = default!;


    public string AirtableApiGetTableContentEndpoint => $"{AirtableApiUrl}/{AirtableBaseId}/{AirtableTableName}";

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(AirtableApiUrl)
            || !string.IsNullOrWhiteSpace(AirtableBaseId)
            || !string.IsNullOrWhiteSpace(AirtableTableName)
            || !string.IsNullOrWhiteSpace(AirtableApiAccessToken);
    }
}
