using System.Reflection;

namespace SpendSmart.Notification;

/// <summary>
/// Represents the notification assembly.
/// </summary>
public static class NotificationAssembly
{
    /// <summary>
    /// Gets the notification assembly.
    /// </summary>
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
