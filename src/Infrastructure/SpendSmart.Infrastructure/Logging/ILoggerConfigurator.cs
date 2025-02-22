using Serilog.Core;

namespace SpendSmart.Infrastructure.Logging;

/// <summary>
/// Represents the logger configurator interface.
/// </summary>
public interface ILoggerConfigurator
{
    /// <summary>
    /// Configures the <see cref="Logger"/> instance.
    /// </summary>
    void Configure();
}
