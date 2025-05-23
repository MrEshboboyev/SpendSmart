﻿using SpendSmart.Domain.Abstractions;

namespace SpendSmart.Domain.Modules.Users.Events;

/// <summary>
/// Represents the event that is raised when a user's primary currency has been changed.
/// </summary>
public sealed class UserPrimaryCurrencyChangedEvent : IEvent
{
    /// <summary>
    /// Gets the user identifier.
    /// </summary>
    public string UserId { get; init; }
}
