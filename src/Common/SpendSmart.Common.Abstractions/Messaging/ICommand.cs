using MediatR;
using SpendSmart.Common.Primitives.Result;

namespace SpendSmart.Common.Abstractions.Messaging;

/// <summary>
/// Represents the command interface.
/// </summary>
/// <typeparam name="TResponse">The command response type.</typeparam>
public interface ICommand<out TResponse> : IRequest<TResponse>
    where TResponse : Result
{
}
