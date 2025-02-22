using Microsoft.AspNetCore.Http;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Common.Primitives.ServiceLifetimes;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Infrastructure.Extensions;
using System;

namespace SpendSmart.Infrastructure.Authentication;

/// <summary>
/// Represents the user identifier provider.
/// </summary>
public sealed class UserInformationProvider : IUserInformationProvider, IScoped
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserInformationProvider"/> class.
    /// </summary>
    /// <param name="httpContextAccessor">The HTTP context accessor.</param>
    public UserInformationProvider(IHttpContextAccessor httpContextAccessor)
    {
        string userIdClaim = httpContextAccessor?.HttpContext?.User?.Identity?.Name;

        IsAuthenticated = Ulid.TryParse(userIdClaim, out Ulid userId);

        UserId = userId;

        PrimaryCurrency = CreatePrimaryCurrency(httpContextAccessor);
    }

    /// <inheritdoc />
    public bool IsAuthenticated { get; }

    /// <inheritdoc />
    public Ulid UserId { get; }

    /// <inheritdoc />
    public Maybe<Currency> PrimaryCurrency { get; }

    private static Maybe<Currency> CreatePrimaryCurrency(IHttpContextAccessor httpContextAccessor)
    {
        string primaryCurrencyString = httpContextAccessor.HttpContext?.User.GetPrimaryCurrency() ?? string.Empty;

        bool parsed = int.TryParse(primaryCurrencyString, out int primaryCurrency);

        if (!parsed || !Currency.ContainsValue(primaryCurrency))
        {
            return Maybe<Currency>.None;
        }

        return Currency.FromValue(primaryCurrency);
    }
}
