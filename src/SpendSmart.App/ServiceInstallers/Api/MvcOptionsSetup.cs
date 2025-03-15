using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SpendSmart.App.ModelBinders.Ulid;

namespace SpendSmart.App.ServiceInstallers.Api;

/// <summary>
/// Represents the <see cref="MvcOptions"/> setup.
/// </summary>
public sealed class MvcOptionsSetup : IConfigureOptions<MvcOptions>
{
    /// <inheritdoc />
    public void Configure(MvcOptions options) => options.ModelBinderProviders.Insert(0, new UlidModelBinderProvider());
}
