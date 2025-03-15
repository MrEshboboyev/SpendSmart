using Microsoft.Extensions.DependencyInjection;
using Raven.Client.Documents;
using Scrutor;
using SpendSmart.App.Abstractions;
using SpendSmart.Application.Abstractions.Data;
using SpendSmart.Persistence;
using SpendSmart.Persistence.Data;
using SpendSmart.Persistence.Providers;
using System;

namespace SpendSmart.App.ServiceInstallers.Persistence;

/// <summary>
/// Represents the persistence services installer.
/// </summary>
public sealed class PersistenceServiceInstaller : IServiceInstaller
{
    private const string RepositoryPostfix = "Repository";
    private const string QueryProcessorPostfix = "QueryProcessor";
    private const string DataRequestsPostfix = "DataRequest";

    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        services.ConfigureOptions<RavenDbOptionsSetup>();

        services.AddSingleton<DocumentStoreProvider>();

        services.AddSingleton(serviceProvider => serviceProvider.GetRequiredService<DocumentStoreProvider>().DocumentStore);

        services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<IDocumentStore>().OpenAsyncSession());

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        AddRepositories(services);

        AddQueryProcessors(services);

        AddDataRequests(services);
    }

    private static void AddRepositories(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(PersistenceAssembly.Assembly)
                .AddClasses(filter => filter.Where(type => type.Name.EndsWith(RepositoryPostfix, StringComparison.Ordinal)))
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsMatchingInterface()
                .WithScopedLifetime());

    private static void AddQueryProcessors(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(PersistenceAssembly.Assembly)
                .AddClasses(filter => filter.Where(type => type.Name.EndsWith(QueryProcessorPostfix, StringComparison.Ordinal)))
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsMatchingInterface()
                .WithScopedLifetime());

    private static void AddDataRequests(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(PersistenceAssembly.Assembly)
                .AddClasses(filter => filter.Where(type => type.Name.EndsWith(DataRequestsPostfix, StringComparison.Ordinal)))
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsMatchingInterface()
                .WithScopedLifetime());
}
