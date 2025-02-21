using SpendSmart.Application.Contracts.Authentication;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Result;

namespace SpendSmart.Application.Commands.Authentication;

/// <summary>
/// Represents the command for refreshing a user's token.
/// </summary>
public sealed class RefreshTokenCommand : ICommand<Result<TokenResponse>>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RefreshTokenCommand"/> class.
    /// </summary>
    /// <param name="refreshToken">The refresh token.</param>
    public RefreshTokenCommand(string refreshToken) => RefreshToken = refreshToken;

    /// <summary>
    /// Gets the refresh token.
    /// </summary>
    public string RefreshToken { get; }
}
