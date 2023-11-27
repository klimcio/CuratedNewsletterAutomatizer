namespace NewIssueService;

public class CuratedLink
{
    public string Url { get; set; } = default!;
    public string Title { get; set; } = default!;
    public string Category { get; set; } = default!;
    public string? Image { get; set; }

    internal static CuratedLink Create(AirTableRecord x) 
        => new()
        {
            Title = x.GetTitle(),
            Category = x.TargetCategory(),
            Url = x.GetUrl(),
            Image = x.GetImage()
        };
}