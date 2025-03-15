using Microsoft.Extensions.DependencyInjection;
using SpendSmart.App.Abstractions;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SpendSmart.App.ServiceInstallers.Authentication;

/// <summary>
/// Represents the authentication services installer.
/// </summary>
public class AuthenticationServiceInstaller : IServiceInstaller
{
    /// <inheritdoc />
    public void InstallServices(IServiceCollection services)
    {
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap[JwtRegisteredClaimNames.Sub] = ClaimTypes.Name;

        services.ConfigureOptions<JwtOptionsSetup>();

        services.ConfigureOptions<JwtBearerOptionsSetup>();

        services.ConfigureOptions<AuthenticationOptionsSetup>();

        services.AddAuthentication().AddJwtBearer();
    }
}
