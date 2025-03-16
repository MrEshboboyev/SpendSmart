using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace SpendSmart.App.ServiceInstallers.Api;

/// <summary>
/// Represents the <see cref="ApiBehaviorOptions"/> setup.
/// </summary>
public sealed class ApiBehaviorOptionsSetup : IConfigureOptions<ApiBehaviorOptions>
{
    /// <inheritdoc />
    public void Configure(ApiBehaviorOptions options) => options.SuppressModelStateInvalidFilter = true;
}
