using System.Collections.Generic;
using SpendSmart.Application.Contracts.Categories;
using SpendSmart.Common.Abstractions.Messaging;

namespace SpendSmart.Application.Queries.Categories;

/// <summary>
/// Represents the query for getting the collection of all supported categories.
/// </summary>
public sealed class GetCategoriesQuery : IQuery<IEnumerable<CategoryResponse>>
{
}
