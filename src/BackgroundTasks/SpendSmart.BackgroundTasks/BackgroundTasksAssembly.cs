﻿using System.Reflection;

namespace SpendSmart.BackgroundTasks;

/// <summary>
/// Represents the background tasks assembly.
/// </summary>
public static class BackgroundTasksAssembly
{
    /// <summary>
    /// Gets the background tasks assembly.
    /// </summary>
    public static readonly Assembly Assembly = Assembly.GetExecutingAssembly();
}
