﻿using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Abstractions.Data;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Transactions;
using SpendSmart.Common.Abstractions.Messaging;
using SpendSmart.Common.Primitives.Maybe;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Modules.Transactions;

namespace SpendSmart.Application.Commands.Handlers.Transactions.DeleteTransaction;

/// <summary>
/// Represents the <see cref="DeleteTransactionCommand"/> handler.
/// </summary>
public sealed class DeleteTransactionCommandHandler : ICommandHandler<DeleteTransactionCommand, Result>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserInformationProvider _userInformationProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTransactionCommandHandler"/> class.
    /// </summary>
    /// <param name="transactionRepository">The transaction repository.</param>
    /// <param name="unitOfWork">The unit of work.</param>
    /// <param name="userInformationProvider">The user information provider.</param>
    public DeleteTransactionCommandHandler(
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork,
        IUserInformationProvider userInformationProvider)
    {
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
        _userInformationProvider = userInformationProvider;
    }

    /// <inheritdoc />
    public async Task<Result> Handle(DeleteTransactionCommand request, CancellationToken cancellationToken)
    {
        Maybe<Transaction> maybeTransaction = await _transactionRepository.GetByIdAsync(request.TransactionId, cancellationToken);

        if (maybeTransaction.HasNoValue)
        {
            return Result.Failure(ValidationErrors.Transaction.NotFound);
        }

        Transaction transaction = maybeTransaction.Value;

        if (transaction.UserId != _userInformationProvider.UserId)
        {
            return Result.Failure(ValidationErrors.User.InvalidPermissions);
        }

        _transactionRepository.Remove(transaction);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
