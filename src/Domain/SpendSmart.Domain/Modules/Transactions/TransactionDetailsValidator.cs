﻿using SpendSmart.Common.Primitives.Result;
using SpendSmart.Common.Primitives.ServiceLifetimes;
using SpendSmart.Domain.Errors;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions.Contracts;

namespace SpendSmart.Domain.Modules.Transactions;

/// <summary>
/// Represents the transaction details validator.
/// </summary>
internal sealed class TransactionDetailsValidator : ITransactionDetailsValidator, ITransient
{
    /// <inheritdoc />
    public Result<ITransactionDetails> Validate(ValidateTransactionDetailsRequest validateTransactionDetailsRequest)
    {
        TransactionType transactionType = TransactionType.FromValue(validateTransactionDetailsRequest.TransactionType).Value;

        Category category = Category.FromValue(validateTransactionDetailsRequest.Category).Value;

        Currency currency = Currency.FromValue(validateTransactionDetailsRequest.Currency).Value;

        var money = new Money(validateTransactionDetailsRequest.Amount, currency);

        Result<Description> descriptionResult = Description.Create(validateTransactionDetailsRequest.Description);

        var result = Result.FirstFailureOrSuccess(
            descriptionResult,
            transactionType.ValidateAmount(money),
            transactionType.ValidateCategory(category));

        if (result.IsFailure)
        {
            return Result.Failure<ITransactionDetails>(result.Error);
        }

        if (!validateTransactionDetailsRequest.User.HasCurrency(currency))
        {
            return Result.Failure<ITransactionDetails>(DomainErrors.User.CurrencyDoesNotExist);
        }

        return new TransactionDetails
        {
            Description = descriptionResult.Value,
            Category = category,
            Money = money,
            OccurredOn = validateTransactionDetailsRequest.OccurredOn,
            TransactionType = transactionType
        };
    }
}
