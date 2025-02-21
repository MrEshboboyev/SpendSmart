using System;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Commands.Handlers.Extensions;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Users;
using SpendSmart.Domain.Modules.Common;
using FluentValidation;

namespace SpendSmart.Application.Commands.Handlers.Users.AddUserCurrency;

/// <summary>
/// Represents the <see cref="AddUserCurrencyCommand"/> validator.
/// </summary>
public sealed class AddUserCurrencyCommandValidator : AbstractValidator<AddUserCurrencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AddUserCurrencyCommandValidator"/> class.
    /// </summary>
    /// <param name="userInformationProvider">The user identifier provider.</param>
    public AddUserCurrencyCommandValidator(IUserInformationProvider userInformationProvider)
    {
        RuleFor(x => x.UserId).NotEmpty().WithError(ValidationErrors.User.IdentifierIsRequired);

        RuleFor(x => x.UserId)
            .Must(x => x == userInformationProvider.UserId)
            .When(x => x.UserId != Ulid.Empty)
            .WithError(ValidationErrors.User.InvalidPermissions);

        RuleFor(x => x.Currency).Must(Currency.ContainsValue).WithError(ValidationErrors.Currency.NotFound);
    }
}
