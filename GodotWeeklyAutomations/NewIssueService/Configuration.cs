using Microsoft.Extensions.Configuration;

namespace NewIssueService;

internal class Configuration
{
    public string AirtableApiAccessToken { get; set; } = default!;
    public string AirtableApiGetTableContentEndpoint { get; set; } = default!;

    public bool IsValid()
    {
        return !string.IsNullOrWhiteSpace(AirtableApiGetTableContentEndpoint) 
            || !string.IsNullOrWhiteSpace(AirtableApiAccessToken);
    }
}
