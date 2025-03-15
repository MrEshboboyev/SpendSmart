using Microsoft.Extensions.DependencyInjection;
using SpendSmart.App.Abstractions;
using SpendSmart.Infrastructure.Logging;

namespace SpendSmart.App.ServiceInstallers.Logging;

/// <summary>
/// Represents the logging services installer.
/// </summary>
public class LoggingServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        services.ConfigureOptions<LoggingOptionsSetup>();

        services.AddTransient<ILoggerConfigurator, LoggerConfigurator>();
    }
}
