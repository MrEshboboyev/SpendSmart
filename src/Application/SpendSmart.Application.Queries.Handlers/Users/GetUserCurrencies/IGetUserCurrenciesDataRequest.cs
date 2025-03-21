﻿using System.Collections.Generic;
using SpendSmart.Application.Queries.Handlers.Abstractions;

namespace SpendSmart.Application.Queries.Handlers.Users.GetUserCurrencies;

/// <summary>
/// Represents the data request interface for getting a user's currencies.
/// </summary>
public interface IGetUserCurrenciesDataRequest : IDataRequest<GetUserCurrenciesRequest, IEnumerable<UserCurrencyModel>>
{
}
