﻿using MediatR;
using SpendSmart.Common.Primitives.Result;

namespace SpendSmart.Common.Abstractions.Messaging;

/// <summary>
/// Represents the command handler interface.
/// </summary>
/// <typeparam name="TCommand">The command type.</typeparam>
/// <typeparam name="TResponse">The command response type.</typeparam>
public interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse>
    where TCommand : ICommand<TResponse>
    where TResponse : Result
{
}
