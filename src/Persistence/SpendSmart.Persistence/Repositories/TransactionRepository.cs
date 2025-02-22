using Raven.Client.Documents.Session;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Domain.Modules.Transactions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SpendSmart.Persistence.Repositories;

/// <summary>
/// Represents the transaction repository.
/// </summary>
public sealed class TransactionRepository : ITransactionRepository
{
    private readonly IAsyncDocumentSession _session;

    /// <summary>
    /// Initializes a new instance of the <see cref="TransactionRepository"/> class.
    /// </summary>
    /// <param name="session">The document session.</param>
    public TransactionRepository(IAsyncDocumentSession session) => _session = session;

    /// <inheritdoc />
    public async Task<Maybe<Transaction>> GetByIdAsync(Ulid transactionId, CancellationToken cancellationToken = default) =>
        await _session.LoadAsync<Transaction>(transactionId.ToString(), cancellationToken);

    /// <inheritdoc />
    public async Task<Maybe<Transaction>> GetByIdWithUserAsync(Ulid transactionId, CancellationToken cancellationToken = default) =>
        await _session.Include(nameof(Transaction.UserId)).LoadAsync<Transaction>(transactionId.ToString(), cancellationToken);

    /// <inheritdoc />
    public async Task AddAsync(Transaction transaction, CancellationToken cancellationToken = default) =>
        await _session.StoreAsync(transaction, cancellationToken);

    /// <inheritdoc />
    public void Remove(Transaction transaction) => _session.Delete(transaction);
}
