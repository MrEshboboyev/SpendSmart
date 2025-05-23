﻿using System;
using SpendSmart.Application.Abstractions.Authentication;
using SpendSmart.Application.Commands.Handlers.Extensions;
using SpendSmart.Application.Commands.Handlers.Validation;
using SpendSmart.Application.Commands.Users;
using FluentValidation;

namespace SpendSmart.Application.Commands.Handlers.Users.ChangeUserPassword;

/// <summary>
/// Represents the <see cref="ChangeUserPasswordCommand"/> validator.
/// </summary>
public sealed class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ChangeUserPasswordCommandValidator"/> class.
    /// </summary>
    /// <param name="userInformationProvider">The user information provider.</param>
    public ChangeUserPasswordCommandValidator(IUserInformationProvider userInformationProvider)
    {
        RuleFor(x => x.UserId).NotEmpty().WithError(ValidationErrors.User.IdentifierIsRequired);

        RuleFor(x => x.UserId)
            .Must(x => x == userInformationProvider.UserId)
            .When(x => x.UserId != Ulid.Empty)
            .WithError(ValidationErrors.User.InvalidPermissions);

        RuleFor(x => x.CurrentPassword).NotEmpty().WithError(ValidationErrors.User.PasswordIsRequired);

        RuleFor(x => x.NewPassword).NotEmpty().WithError(ValidationErrors.User.PasswordIsRequired);

        RuleFor(x => x.ConfirmationPassword)
            .Equal(x => x.NewPassword)
            .When(x => !string.IsNullOrWhiteSpace(x.NewPassword))
            .WithError(ValidationErrors.User.PasswordAndConfirmationPasswordMustMatch);
    }
}
