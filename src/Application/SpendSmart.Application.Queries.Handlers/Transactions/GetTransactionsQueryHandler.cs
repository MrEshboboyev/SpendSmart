using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Application.Queries.Processors.Transactions;
using SpendSmart.Application.Queries.Transactions;
using SpendSmart.Common.Abstractions.Messaging;

namespace SpendSmart.Application.Queries.Handlers.Transactions;

/// <summary>
/// Represents the <see cref="GetTransactionsQuery"/> handler.
/// </summary>
public sealed class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, TransactionListResponse>
{
    private readonly IGetTransactionsQueryProcessor _processor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTransactionsQueryHandler"/> class.
    /// </summary>
    /// <param name="processor">The get transactions query processor.</param>
    public GetTransactionsQueryHandler(IGetTransactionsQueryProcessor processor) => _processor = processor;

    /// <inheritdoc />
    public async Task<TransactionListResponse> Handle(GetTransactionsQuery request, CancellationToken cancellationToken) =>
        await _processor.Process(request, cancellationToken);
}
