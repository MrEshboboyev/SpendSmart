using System.Reflection;

namespace SpendSmart.Application.Commands.Handlers;

/// <summary>
/// Represents the command handlers assembly.
/// </summary>
public static class CommandHandlersAssembly
{
    /// <summary>
    /// Gets the command handlers assembly.
    /// </summary>
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
