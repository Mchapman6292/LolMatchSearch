using API.Configuration.APIStartConfigurations;

public class Program
{
    public static async Task Main(string[] args)
    {
        // HostBuilder, sets up basic config. Includes logging DI container, logging.

        var host = APIStartConfiguration.CreateHostBuilder(args).Build();
        await host.RunAsync();
    }
}