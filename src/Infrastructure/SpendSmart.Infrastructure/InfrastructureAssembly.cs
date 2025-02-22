using System.Reflection;

namespace SpendSmart.Infrastructure;

/// <summary>
/// Represents the infrastructure assembly.
/// </summary>
public static class InfrastructureAssembly
{
    /// <summary>
    /// Gets the infrastructure assembly.
    /// </summary>
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
