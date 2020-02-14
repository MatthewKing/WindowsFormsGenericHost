using System;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WindowsFormsGenericHost
{
    public static class HostBuilderExtensions
    {
        public static IHostBuilder UseWindowsFormsLifetime<TForm>(
            this IHostBuilder hostBuilder,
            Action<WindowsFormsApplicationOptions> configureApplication = null,
            Action<WindowsFormsLifetimeOptions> configureLifetime = null)
            where TForm : Form
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ApplicationContext>(c => new ApplicationContext(c.GetRequiredService<TForm>()));

                services.AddSingleton<IHostLifetime, WindowsFormsLifetime>();
                
                services.AddHostedService<WindowsFormsApplicationHostedService>();

                if (configureApplication != null)
                {
                    services.Configure(configureApplication);
                }

                if (configureLifetime != null)
                {
                    services.Configure(configureLifetime);
                }
            });
        }

        public static IHostBuilder UseWindowsFormsApplicationContextLifetime<TApplicationContext>(
            this IHostBuilder hostBuilder,
            Action<WindowsFormsApplicationOptions> configureApplication = null,
            Action<WindowsFormsLifetimeOptions> configureLifetime = null)
            where TApplicationContext : ApplicationContext
        {
            return hostBuilder.ConfigureServices((context, services) =>
            {
                services.AddSingleton<ApplicationContext, TApplicationContext>();

                services.AddSingleton<IHostLifetime, WindowsFormsLifetime>();
                
                services.AddHostedService<WindowsFormsApplicationHostedService>();

                if (configureApplication != null)
                {
                    services.Configure(configureApplication);
                }

                if (configureLifetime != null)
                {
                    services.Configure(configureLifetime);
                }
            });
        }
    }
}
