using SpendSmart.Application.Contracts.Authentication;
using SpendSmart.Domain.Modules.Users;

namespace SpendSmart.Application.Abstractions.Authentication;

/// <summary>
/// Represents the JWT provider interface.
/// </summary>
public interface IJwtProvider
{
    /// <summary>
    /// Gets the access tokens for the specified user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The access tokens for the specified user.</returns>
    AccessTokens GetAccessTokens(User user);
}
