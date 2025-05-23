﻿using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Application.Queries.Processors.Transactions;
using SpendSmart.Application.Queries.Transactions;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;

namespace SpendSmart.Application.Queries.Handlers.Transactions;

/// <summary>
/// Represents the <see cref="GetCurrentMonthTransactionSummaryQuery"/> handler.
/// </summary>
public sealed class GetCurrentMonthTransactionSummaryQueryHandler
    : IQueryHandler<GetCurrentMonthTransactionSummaryQuery, Maybe<TransactionSummaryResponse>>
{
    private readonly IGetCurrentMonthTransactionSummaryQueryProcessor _processor;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetCurrentMonthTransactionSummaryQueryHandler"/> class.
    /// </summary>
    /// <param name="processor">The get current month transaction summary query processor.</param>
    public GetCurrentMonthTransactionSummaryQueryHandler(IGetCurrentMonthTransactionSummaryQueryProcessor processor) =>
        _processor = processor;

    /// <inheritdoc />
    public async Task<Maybe<TransactionSummaryResponse>> Handle(
        GetCurrentMonthTransactionSummaryQuery request,
        CancellationToken cancellationToken) =>
        await _processor.Process(request, cancellationToken);
}
