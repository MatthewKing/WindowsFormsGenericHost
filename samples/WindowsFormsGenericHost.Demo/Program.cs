using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WindowsFormsGenericHost.Demo;

public static class Program
{
    public static void Main(string[] args)
    {
        using var host = Host.CreateDefaultBuilder()
            .ConfigureServices(ConfigureServices)
            .UseWindowsFormsLifetime<MainForm>()
            .Build();

        host.Run();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddForms();
    }
}
