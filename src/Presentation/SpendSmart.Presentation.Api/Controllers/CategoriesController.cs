using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.Categories;
using SpendSmart.Application.Queries.Categories;
using SpendSmart.Authorization.Abstractions;
using SpendSmart.Authorization.Attributes;
using SpendSmart.Common.Primitives.Extensions;
using SpendSmart.Presentation.Api.Constants;
using SpendSmart.Presentation.Api.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SpendSmart.Presentation.Api.Controllers;

/// <summary>
/// Represents the categories resource controller.
/// </summary>
public sealed class CategoriesController : ApiController
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CategoriesController"/> class.
    /// </summary>
    /// <param name="sender">The sender.</param>
    public CategoriesController(ISender sender)
        : base(sender)
    {
    }

    /// <summary>
    /// Gets the readonly collection of all supported categories.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The collection of all supported categories.</returns>
    [HasPermission(Permission.CategoryRead)]
    [HttpGet(ApiRoutes.Categories.GetCategories)]
    [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken) =>
        await Sender.Send(new GetCategoriesQuery(), cancellationToken).Map(Ok);
}
