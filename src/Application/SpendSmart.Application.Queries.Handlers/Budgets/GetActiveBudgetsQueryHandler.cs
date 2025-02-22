using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Application.Queries.Budgets;
using SpendSmart.Application.Queries.Processors.Budgets;
using SpendSmart.Common.Abstractions.Messaging;

namespace SpendSmart.Application.Queries.Handlers.Budgets;

/// <summary>
/// Represents the <see cref="GetActiveBudgetsQuery"/> handler.
/// </summary>
internal sealed class GetActiveBudgetsQueryHandler : IQueryHandler<GetActiveBudgetsQuery, IEnumerable<BudgetListItemResponse>>
{
    private readonly IGetActiveBudgetsQueryProcessor _processor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetActiveBudgetsQueryHandler"/> class.
    /// </summary>
    /// <param name="processor">The query processor.</param>
    public GetActiveBudgetsQueryHandler(IGetActiveBudgetsQueryProcessor processor) => _processor = processor;

    /// <inheritdoc />
    public async Task<IEnumerable<BudgetListItemResponse>> Handle(
        GetActiveBudgetsQuery request,
        CancellationToken cancellationToken) =>
        await _processor.Process(request, cancellationToken);
}
