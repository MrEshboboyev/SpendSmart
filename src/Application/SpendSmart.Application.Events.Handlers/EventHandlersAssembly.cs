using System.Reflection;

namespace SpendSmart.Application.Events.Handlers;

/// <summary>
/// Represents the event handlers assembly.
/// </summary>
public static class EventHandlersAssembly
{
    /// <summary>
    /// Gets the event handlers assembly.
    /// </summary>
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
