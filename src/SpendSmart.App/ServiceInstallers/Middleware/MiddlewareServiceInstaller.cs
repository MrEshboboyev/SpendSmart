﻿using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;
using SpendSmart.App.Abstractions;
using System.Reflection;

namespace SpendSmart.App.ServiceInstallers.Middleware;

/// <summary>
/// Represents the middleware services installer.
/// </summary>
public sealed class MiddlewareServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services) =>
        services.Scan(scan =>
            scan.FromAssemblies(Assembly.GetExecutingAssembly())
                .AddClasses(filter => filter.AssignableTo<IMiddleware>())
                .UsingRegistrationStrategy(RegistrationStrategy.Throw)
                .AsSelf()
                .WithTransientLifetime());
}
