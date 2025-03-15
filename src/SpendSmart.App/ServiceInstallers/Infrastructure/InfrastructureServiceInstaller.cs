using Microsoft.Extensions.DependencyInjection;
using SpendSmart.App.Abstractions;
using SpendSmart.App.Extensions;
using SpendSmart.Infrastructure;

namespace SpendSmart.App.ServiceInstallers.Infrastructure;

/// <summary>
/// Represents the infrastructure services installer.
/// </summary>
public class InfrastructureServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        services.AddTransientAsMatchingInterface(InfrastructureAssembly.Assembly);

        services.AddScopedAsMatchingInterface(InfrastructureAssembly.Assembly);
    }
}
