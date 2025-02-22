using System.Collections.Generic;
using SpendSmart.Application.Contracts.Currencies;
using SpendSmart.Common.Abstractions.Messaging;

namespace SpendSmart.Application.Queries.Currencies;

/// <summary>
/// Represents the query for getting the collection of all supported currencies.
/// </summary>
public sealed class GetCurrenciesQuery : IQuery<IEnumerable<CurrencyResponse>>
{
}
