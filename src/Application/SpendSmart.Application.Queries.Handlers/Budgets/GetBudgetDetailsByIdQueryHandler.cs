using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Application.Queries.Budgets;
using SpendSmart.Application.Queries.Processors.Budgets;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Handlers.Budgets;

/// <summary>
/// Represents the <see cref="GetBudgetDetailsByIdQuery"/> handler.
/// </summary>
public sealed class GetBudgetDetailsByIdQueryHandler : IQueryHandler<GetBudgetDetailsByIdQuery, Maybe<BudgetDetailsResponse>>
{
    private readonly IGetBudgetDetailsByIdQueryProcessor _processor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetDetailsByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="processor">The get budget by identifier query processor.</param>
    public GetBudgetDetailsByIdQueryHandler(IGetBudgetDetailsByIdQueryProcessor processor) => _processor = processor;

    /// <inheritdoc />
    public async Task<Maybe<BudgetDetailsResponse>> Handle(GetBudgetDetailsByIdQuery request, CancellationToken cancellationToken) =>
        await _processor.Process(request, cancellationToken);
}
