using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SpendSmart.App.Abstractions;
using SpendSmart.App.Extensions;
using SpendSmart.BackgroundTasks;

namespace SpendSmart.App.ServiceInstallers.BackgroundTasks;

/// <summary>
/// Represents the background tasks services installer.
/// </summary>
public sealed class BackgroundTasksServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        services.ConfigureOptions<QuartzHostedServiceOptionsSetup>();

        services.ConfigureOptions<MessageProcessingJobOptionsSetup>();

        services.ConfigureOptions<MessageProcessingJobSetup>();

        //services.AddQuartz(configure => configure.UseMicrosoftDependencyInjectionJobFactory());

        services.AddQuartzHostedService();

        services.AddTransientAsMatchingInterface(BackgroundTasksAssembly.Assembly);

        services.AddScopedAsMatchingInterface(BackgroundTasksAssembly.Assembly);
    }
}
