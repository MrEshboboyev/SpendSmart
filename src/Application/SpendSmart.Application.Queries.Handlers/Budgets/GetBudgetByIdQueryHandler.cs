using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.Budgets;
using SpendSmart.Application.Queries.Budgets;
using SpendSmart.Application.Queries.Processors.Budgets;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Handlers.Budgets;

/// <summary>
/// Represents the <see cref="GetBudgetByIdQuery"/> handler.
/// </summary>
public sealed class GetBudgetByIdQueryHandler : IQueryHandler<GetBudgetByIdQuery, Maybe<BudgetResponse>>
{
    private readonly IGetBudgetByIdQueryProcessor _processor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetBudgetByIdQueryHandler"/> class.
    /// </summary>
    /// <param name="processor">The get budget by identifier query processor.</param>
    public GetBudgetByIdQueryHandler(IGetBudgetByIdQueryProcessor processor) => _processor = processor;

    /// <inheritdoc />
    public async Task<Maybe<BudgetResponse>> Handle(GetBudgetByIdQuery request, CancellationToken cancellationToken) =>
        await _processor.Process(request, cancellationToken);
}
