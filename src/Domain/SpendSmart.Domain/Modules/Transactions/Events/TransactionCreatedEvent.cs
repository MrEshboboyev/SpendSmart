﻿using SpendSmart.Domain.Abstractions;
using System;

namespace SpendSmart.Domain.Modules.Transactions.Events;

/// <summary>
/// Represents the event that is raised when a transaction is created.
/// </summary>
public sealed class TransactionCreatedEvent : IEvent
{
    /// <summary>
    /// Gets the transaction identifier.
    /// </summary>
    public Ulid TransactionId { get; init; }
}
