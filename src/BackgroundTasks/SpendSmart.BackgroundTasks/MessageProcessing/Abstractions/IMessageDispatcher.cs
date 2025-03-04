﻿using System;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Domain.Modules.Messages;

namespace SpendSmart.BackgroundTasks.MessageProcessing.Abstractions;

/// <summary>
/// Represents the message dispatcher interface.
/// </summary>
public interface IMessageDispatcher
{
    /// <summary>
    /// Dispatches the specified message for processing.
    /// </summary>
    /// <param name="message">The message to be processed.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The maybe instance that may contain the exception that occurred during dispatch.</returns>
    Task<Maybe<Exception>> DispatchAsync(Message message, CancellationToken cancellationToken);
}
