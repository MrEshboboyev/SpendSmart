using Microsoft.Extensions.DependencyInjection;
using SpendSmart.App.Abstractions;
using SpendSmart.App.Extensions;
using SpendSmart.Notification;

namespace SpendSmart.App.ServiceInstallers.Notification;

/// <summary>
/// Represents the email services installer.
/// </summary>
public sealed class NotificationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        services.ConfigureOptions<EmailOptionsSetup>();

        services.ConfigureOptions<AlertOptionsSetup>();

        services.AddTransientAsMatchingInterface(NotificationAssembly.Assembly);
    }
}
