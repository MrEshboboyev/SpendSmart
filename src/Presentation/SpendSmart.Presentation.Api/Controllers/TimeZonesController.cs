using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SpendSmart.Application.Queries.TimeZones;
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
/// Represents the authentication controller.
/// </summary>
public sealed class TimeZonesController : ApiController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TimeZonesController"/> class.
    /// </summary>
    /// <param name="sender">The sender.</param>
    public TimeZonesController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Gets the readonly collection of all supported time zones.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of all supported time zones.</returns>
    [HasPermission(Permission.TimeZoneRead)]
    [HttpGet(ApiRoutes.TimeZones.GetTimeZones)]
    [ProducesResponseType(typeof(IEnumerable<GetTimeZonesQuery>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetTimeZones(CancellationToken cancellationToken) =>
        await Sender.Send(new GetTimeZonesQuery(), cancellationToken).Map(Ok);
}
