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
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }
          
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
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }

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
        public static IHostBuilder ConfigureAppHostDefaults(this IHostBuilder builder, Action<IHostBuilder> configure)
        {
            configure.Invoke(builder);
            return builder;
        }
        public static IHostBuilder UseStartup<T>(this IHostBuilder hostBuilder,
             Action<WindowsFormsApplicationOptions> configureApplication = null,
            Action<WindowsFormsLifetimeOptions> configureLifetime = null)
        {
            if (hostBuilder == null)
            {
                throw new ArgumentNullException(nameof(hostBuilder));
            }
            return hostBuilder.ConfigureServices((context, services) =>
            {
                dynamic d = Activator.CreateInstance(typeof(T), context.Configuration);
                var env = new ApplicationContext(d.MainForm);
                services.AddSingleton(c => env);

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
                services.AddForms();
                d.ConfigureServices(services);
                d.Configure(hostBuilder, context.HostingEnvironment);
            });
        }
    }
}
