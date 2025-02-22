using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.Categories;
using SpendSmart.Application.Queries.Categories;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Domain.Modules.Common;

namespace SpendSmart.Application.Queries.Handlers.Categories;

/// <summary>
/// Represents the <see cref="GetCategoriesQuery"/> handler.
/// </summary>
public sealed class GetCategoriesQueryHandler : IQueryHandler<GetCategoriesQuery, IEnumerable<CategoryResponse>>
{
    /// <inheritdoc />
    public Task<IEnumerable<CategoryResponse>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<CategoryResponse> categories = [.. Category
            .List
            .Select(x => new CategoryResponse
            {
                Id = x.Value,
                Name = x.Name,
                IsDefault = x.IsDefault,
                IsExpense = x.IsExpense
            })];

        return Task.FromResult(categories);
    }
}
