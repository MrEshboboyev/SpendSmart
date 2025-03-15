using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using SpendSmart.App.Abstractions;
using SpendSmart.Application.Commands.Handlers;

namespace SpendSmart.App.ServiceInstallers.Validation;

/// <summary>
/// Represents the validation services installer.
/// </summary>
public sealed class ValidationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services) =>
        services.AddValidatorsFromAssembly(CommandHandlersAssembly.Assembly);
}
