using System;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Commands.Handlers.Extensions;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Users;
using SpendSmart.Domain.Modules.Common;
using FluentValidation;

namespace SpendSmart.Application.Commands.Handlers.Users.ChangeUserPrimaryCurrency;

/// <summary>
/// Represents the <see cref="ChangeUserPrimaryCurrencyCommand"/> validator.
/// </summary>
public sealed class ChangeUserPrimaryCurrencyCommandValidator : AbstractValidator<ChangeUserPrimaryCurrencyCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeUserPrimaryCurrencyCommandValidator"/> class.
    /// </summary>
    /// <param name="userInformationProvider">The user identifier provider.</param>
    public ChangeUserPrimaryCurrencyCommandValidator(IUserInformationProvider userInformationProvider)
    {
        RuleFor(x => x.UserId).NotEmpty().WithError(ValidationErrors.User.IdentifierIsRequired);

        RuleFor(x => x.UserId)
            .Must(x => x == userInformationProvider.UserId)
            .When(x => x.UserId != Ulid.Empty)
            .WithError(ValidationErrors.User.InvalidPermissions);

        RuleFor(x => x.Currency).Must(Currency.ContainsValue).WithError(ValidationErrors.Currency.NotFound);
    }
}
