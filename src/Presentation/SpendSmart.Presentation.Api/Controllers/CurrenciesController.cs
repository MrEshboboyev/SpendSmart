using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Application.Contracts.Currencies;
using SpendSmart.Application.Queries.Currencies;
using SpendSmart.Authorization.Abstractions;
using SpendSmart.Authorization.Attributes;
using SpendSmart.Common.Primitives.Extensions;
using SpendSmart.Presentation.Api.Constants;
using SpendSmart.Presentation.Api.Infrastructure;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Presentation.Api.Controllers;

/// <summary>
/// Represents the currencies resource controller.
/// </summary>
public sealed class CurrenciesController : ApiController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CurrenciesController"/> class.
    /// </summary>
    /// <param name="sender">The sender.</param>
    public CurrenciesController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Gets the readonly collection of all supported currencies.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of all supported currencies.</returns>
    [HasPermission(Permission.CurrencyRead)]
    [HttpGet(ApiRoutes.Currencies.GetCurrencies)]
    [ProducesResponseType(typeof(IEnumerable<CurrencyResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCurrencies(CancellationToken cancellationToken) =>
        await Sender.Send(new GetCurrenciesQuery(), cancellationToken).Map(Ok);
}
