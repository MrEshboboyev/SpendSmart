using SpendSmart.Application.Commands.Handlers.Extensions;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Transactions;
using SpendSmart.Domain.Modules.Common;
using FluentValidation;

namespace SpendSmart.Application.Commands.Handlers.Transactions.UpdateTransaction;

/// <summary>
/// Represents the <see cref="UpdateTransactionCommand"/> validator.
/// </summary>
public sealed class UpdateTransactionCommandValidator : AbstractValidator<UpdateTransactionCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTransactionCommandValidator"/> class.
    /// </summary>
    public UpdateTransactionCommandValidator()
    {
        RuleFor(x => x.TransactionId).NotEmpty().WithError(ValidationErrors.Transaction.IdentifierIsRequired);

        RuleFor(x => x.Description).NotEmpty().WithError(ValidationErrors.Transaction.DescriptionIsRequired);

        RuleFor(x => x.Category).Must(Category.ContainsValue).WithError(ValidationErrors.Category.NotFound);

        RuleFor(x => x.Currency).Must(Currency.ContainsValue).WithError(ValidationErrors.Currency.NotFound);

        RuleFor(x => x.OccurredOn).NotEmpty().WithError(ValidationErrors.Transaction.OccurredOnDateIsRequired);
    }
}
