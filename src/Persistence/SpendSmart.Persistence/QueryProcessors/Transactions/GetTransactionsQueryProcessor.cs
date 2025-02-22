using Raven.Client.Documents;
using Raven.Client.Documents.Linq;
using Raven.Client.Documents.Session;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Contracts.Transactions;
using SpendSmart.Application.Queries.Processors.Transactions;
using SpendSmart.Application.Queries.Transactions;
using SpendSmart.Application.Queries.Utility;
using SpendSmart.Common.Abstractions.Constants;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Persistence.Indexes.Transactions;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Persistence.QueryProcessors.Transactions;

/// <summary>
/// Represents the <see cref="GetTransactionsQuery"/> processor.
/// </summary>
internal sealed class GetTransactionsQueryProcessor : IGetTransactionsQueryProcessor
{
    private readonly IAsyncDocumentSession _session;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="GetTransactionsQueryProcessor"/> class.
    /// </summary>
    /// <param name="session">The document session.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public GetTransactionsQueryProcessor(IAsyncDocumentSession session, IUserInformationProvider userInformationProvider)
    {
        _session = session;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<TransactionListResponse> Process(GetTransactionsQuery query, CancellationToken cancellationToken = default)
    {
        if (query.UserId != _userInformationProvider.UserId)
        {
            return new TransactionListResponse([]);
        }

        var transactions = await _session
            .Query<Transaction, Transactions_Search>()
            .Where(x =>
                x.UserId == query.UserId &&
                (x.OccurredOn < query.OccurredOn ||
                 x.OccurredOn == query.OccurredOn && x.CreatedOnUtc <= query.CreatedOnUtc))
            .OrderByDescending(x => x.OccurredOn)
            .ThenByDescending(x => x.CreatedOnUtc)
            .Select(x => new
            {
                x.Id,
                x.Description,
                Category = new
                {
                    x.Category.Name
                },
                x.Money,
                x.OccurredOn,
                x.CreatedOnUtc
            })
            .Take(query.Limit)
            .ToArrayAsync(cancellationToken);

        TransactionListResponse.TransactionListItem[] transactionListItems = [.. transactions
            .Select(x =>
                new TransactionListResponse.TransactionListItem(x.Id,
                                                                x.Description.Value,
                                                                x.Category.Name,
                                                                x.Money,
                                                                x.OccurredOn))];

        if (transactionListItems.Length < query.Limit)
        {
            return new TransactionListResponse(transactionListItems);
        }

        var lastTransaction = transactions[^1];

        string cursor = Cursor.Create(
            lastTransaction.OccurredOn.ToString(DateTimeFormats.Date,
                                                CultureInfo.InvariantCulture),
            lastTransaction.CreatedOnUtc.ToString(DateTimeFormats.DateTimeWithMilliseconds,
                                                  CultureInfo.InvariantCulture));

        return new TransactionListResponse(transactionListItems[..^1], cursor);
    }
}
