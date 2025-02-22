using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.Currencies;
using SpendSmart.Application.Queries.Currencies;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Domain.Modules.Common;

namespace SpendSmart.Application.Queries.Handlers.Currencies;

/// <summary>
/// Represents the <see cref="GetCurrenciesQuery"/> handler.
/// </summary>
public sealed class GetCurrenciesQueryHandler : IQueryHandler<GetCurrenciesQuery, IEnumerable<CurrencyResponse>>
{
    /// <inheritdoc />
    public Task<IEnumerable<CurrencyResponse>> Handle(GetCurrenciesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CurrencyResponse> categories = [.. Currency
            .List
            .Select(x => new CurrencyResponse
            {
                Id = x.Value,
                Name = x.Name,
                Code = x.Code
            })];

        return Task.FromResult(categories);
    }
}
