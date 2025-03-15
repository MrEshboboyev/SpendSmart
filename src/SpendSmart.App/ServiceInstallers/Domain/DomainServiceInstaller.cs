using Microsoft.Extensions.DependencyInjection;
using SpendSmart.App.Abstractions;
using SpendSmart.App.Extensions;
using SpendSmart.Domain;

namespace SpendSmart.App.ServiceInstallers.Domain;

/// <summary>
/// Represents the domain service installer.
/// </summary>
public sealed class DomainServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        services.AddTransientAsMatchingInterface(DomainAssembly.Assembly);

        services.AddScopedAsMatchingInterface(DomainAssembly.Assembly);
    }
}
