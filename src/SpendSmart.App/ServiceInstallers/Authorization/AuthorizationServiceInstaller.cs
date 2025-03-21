﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using SpendSmart.App.Abstractions;
using SpendSmart.App.Extensions;
using SpendSmart.Authorization;

namespace SpendSmart.App.ServiceInstallers.Authorization;

/// <summary>
/// Represents the authorization services installer.
/// </summary>
public sealed class AuthorizationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        services.AddAuthorization();

        services.AddTransientAsMatchingInterface(AuthorizationAssembly.Assembly);

        AddAuthorizationPolicyProvider(services);

        AddAuthorizationHandler(services);
    }

    private static void AddAuthorizationPolicyProvider(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(AuthorizationAssembly.Assembly)
                .AddClasses(filter => filter.AssignableTo<IAuthorizationPolicyProvider>(), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Replace())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());

    private static void AddAuthorizationHandler(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(AuthorizationAssembly.Assembly)
                .AddClasses(filter => filter.AssignableTo<IAuthorizationHandler>(), false)
                .UsingRegistrationStrategy(RegistrationStrategy.Replace())
                .AsImplementedInterfaces()
                .WithSingletonLifetime());
}
