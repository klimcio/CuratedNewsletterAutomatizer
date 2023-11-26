using NewIssueService;

namespace NewsletterAutomations.Tests;

[TestFixture]
public class YouTubeIdExtractionTests
{
    [TestCase("https://www.youtube.com/watch?v=TJlH0cghcUc")]
    [TestCase("https://www.youtube.com/watch?v=TJlH0cghcUc&list=WL&index=20")]
    [TestCase("https://youtu.be/TJlH0cghcUc?si=5HQ00FMm6qJk_WJy")]
    [TestCase("https://youtu.be/TJlH0cghcUc")]
    public void MethodReturnsProperYouTubeId(string youtubeUrl) 
        => Assert.That(Extensions.GetYoutubeClipId(youtubeUrl), Is.EqualTo("TJlH0cghcUc"));
}