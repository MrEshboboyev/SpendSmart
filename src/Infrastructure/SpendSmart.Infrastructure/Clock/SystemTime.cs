using SpendSmart.Common.Abstractions.Clock;
using SpendSmart.Common.Primitives.ServiceLifetimes;

namespace SpendSmart.Infrastructure.Clock;

/// <summary>
/// Represents the current machine date and time.
/// </summary>
public sealed class SystemTime : ISystemTime, ITransient
{
    /// <inheritdoc />
    public System.DateTime UtcNow => System.DateTime.UtcNow;
}
