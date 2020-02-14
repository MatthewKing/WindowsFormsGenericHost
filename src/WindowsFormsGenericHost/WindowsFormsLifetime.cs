using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace WindowsFormsGenericHost
{
    // An attempt to mimic the structure and functionality of the official ConsoleLifetime
    // https://github.com/dotnet/extensions/blob/master/src/Hosting/Hosting/src/Internal/ConsoleLifetime.cs

    public class WindowsFormsLifetime : IHostLifetime, IDisposable
    {
        public WindowsFormsLifetimeOptions Options { get; }
        public IHostEnvironment Environment { get; }
        public IHostApplicationLifetime ApplicationLifetime { get; }
        public HostOptions HostOptions { get; }
        private ILogger Logger { get; }

        private readonly ManualResetEvent _shutdownBlock;

        private CancellationTokenRegistration _applicationStartedRegistration;
        private CancellationTokenRegistration _applicationStoppingRegistration;

        public WindowsFormsLifetime(
            IOptions<WindowsFormsLifetimeOptions> options,
            IHostEnvironment environment,
            IHostApplicationLifetime applicationLifetime,
            IOptions<HostOptions> hostOptions,
            ILoggerFactory loggerFactory)
        {
            Options = options?.Value ?? throw new ArgumentNullException(nameof(options));
            Environment = environment ?? throw new ArgumentNullException(nameof(environment));
            ApplicationLifetime = applicationLifetime ?? throw new ArgumentNullException(nameof(applicationLifetime));
            HostOptions = hostOptions?.Value ?? throw new ArgumentNullException(nameof(hostOptions));
            Logger = loggerFactory.CreateLogger("Microsoft.Hosting.Lifetime");

            _shutdownBlock = new ManualResetEvent(false);
            _applicationStartedRegistration = default;
            _applicationStoppingRegistration = default;
        }

        public Task WaitForStartAsync(CancellationToken cancellationToken)
        {
            if (!Options.SuppressStatusMessages)
            {
                _applicationStartedRegistration = ApplicationLifetime.ApplicationStarted.Register(state =>
                {
                    ((WindowsFormsLifetime)state).OnApplicationStarted();
                },
                this);

                _applicationStoppingRegistration = ApplicationLifetime.ApplicationStopping.Register(state =>
                {
                    ((WindowsFormsLifetime)state).OnApplicationStopping();
                },
                this);
            }

            Application.ApplicationExit += OnExit;

            return Task.CompletedTask;
        }

        private void OnApplicationStarted()
        {
            Logger.LogInformation("Application started. Close the main form to shut down.");
            Logger.LogInformation("Hosting environment: {envName}", Environment.EnvironmentName);
            Logger.LogInformation("Content root path: {contentRoot}", Environment.ContentRootPath);
        }

        private void OnApplicationStopping()
        {
            Logger.LogInformation("Application is shutting down...");
        }

        private void OnExit(object sender, EventArgs e)
        {
            ApplicationLifetime.StopApplication();

            if (!_shutdownBlock.WaitOne(HostOptions.ShutdownTimeout))
            {
                Logger.LogInformation("Waiting for the host to be disposed. Ensure all 'IHost' instances are wrapped in 'using' blocks.");
            }

            _shutdownBlock.WaitOne();

            System.Environment.ExitCode = 0;
        }


        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _shutdownBlock.Set();

            Application.ApplicationExit -= OnExit;

            _applicationStartedRegistration.Dispose();
            _applicationStoppingRegistration.Dispose();
        }
    }
}
