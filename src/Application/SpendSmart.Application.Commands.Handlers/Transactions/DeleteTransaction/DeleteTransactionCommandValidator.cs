using SpendSmart.Application.Commands.Handlers.Extensions;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Transactions;
using FluentValidation;

namespace SpendSmart.Application.Commands.Handlers.Transactions.DeleteTransaction;

/// <summary>
/// Represents the <see cref="DeleteTransactionCommand"/> validator.
/// </summary>
public sealed class DeleteTransactionCommandValidator : AbstractValidator<DeleteTransactionCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTransactionCommandValidator"/> class.
    /// </summary>
    public DeleteTransactionCommandValidator() =>
        RuleFor(x => x.TransactionId).NotEmpty().WithError(ValidationErrors.Transaction.IdentifierIsRequired);
}
