﻿using SpendSmart.Domain.Abstractions;

namespace SpendSmart.Domain.Modules.Users.Events;

/// <summary>
/// Represents the event that is raised when a user currency has been removed.
/// </summary>
public sealed class UserCurrencyRemovedEvent : IEvent
{
    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    public string UserId { get; init; }

    /// <summary>
    /// Gets the currency.
    /// </summary>
    public int Currency { get; init; }
}
