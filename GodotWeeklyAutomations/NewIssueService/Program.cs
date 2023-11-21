using Microsoft.Extensions.Configuration;

namespace NewIssueService;

internal class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile($"appsettings.json")
            .Build()
            .GetRequiredSection("Settings")
            .Get<Configuration>();



        Console.WriteLine("Hello, World!");
    }
}
