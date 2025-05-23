﻿using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Contracts.Users;
using SpendSmart.Application.Queries.Users;
using SpendSmart.Common.Abstractions.Messaging;

namespace SpendSmart.Application.Queries.Handlers.Users.GetUserCurrencies;

/// <summary>
/// Represents the <see cref="GetUserCurrenciesQuery"/> handler.
/// </summary>
public sealed class GetUserCurrenciesQueryHandler : IQueryHandler<GetUserCurrenciesQuery, IEnumerable<UserCurrencyResponse>>
{
    private readonly IUserInformationProvider _userInformationProvider;
    private readonly IGetUserCurrenciesDataRequest _request;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetUserCurrenciesQueryHandler"/> class.
    /// </summary>
    /// <param name="userInformationProvider">The user information provider.</param>
    /// <param name="request">The get user currencies data request.</param>
    public GetUserCurrenciesQueryHandler(IUserInformationProvider userInformationProvider, IGetUserCurrenciesDataRequest request)
    {
        _request = request;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<UserCurrencyResponse>> Handle(
        GetUserCurrenciesQuery request,
        CancellationToken cancellationToken)
    {
        if (request.UserId != _userInformationProvider.UserId)
        {
            return [];
        }

        IEnumerable<UserCurrencyModel> userCurrencyModels = await _request.GetAsync(
            new GetUserCurrenciesRequest(request.UserId),
            cancellationToken);

        if (userCurrencyModels is null)
        {
            return Enumerable.Empty<UserCurrencyResponse>();
        }

        UserCurrencyResponse[] userCurrencyResponses = userCurrencyModels
            .Select(x => new UserCurrencyResponse(x.Value, x.Name, x.Code, x.IsPrimary))
            .OrderByDescending(x => x.IsPrimary)
            .ToArray();

        return userCurrencyResponses;
    }
}
