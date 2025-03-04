﻿using FluentAssertions;
using SpendSmart.Common.Primitives.Errors;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Errors;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Domain.Modules.Transactions.Contracts;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Descriptions;
using SpendSmart.Domain.UnitTests.TestData.Transactions;
using SpendSmart.Domain.UnitTests.TestData.Users;
using Xunit;

namespace SpendSmart.Domain.UnitTests.Modules.Transactions;

public class TransactionDetailsValidatorTests
{
    [Fact]
    public void Validate_ShouldReturnFailureResult_WhenDescriptionIsLongerThanAllowed()
    {
        // Arrange
        var validateTransactionDetailsRequest = new ValidateTransactionDetailsRequest(
            default,
            DescriptionTestData.LongerThanAllowedDescription,
            Category.None.Value,
            default,
            CurrencyTestData.DefaultCurrency.Value,
            default,
            TransactionType.Expense.Value);

        var transactionDetailsValidator = new TransactionDetailsValidator();

        // Act
        Result result = transactionDetailsValidator.Validate(validateTransactionDetailsRequest);

        // Assert
        result.IsFailure.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(UserWithNoCurrencyData))]
    public void Validate_ShouldReturnFailureResult_WhenUserDoesNotHaveCurrency(User user, Currency currency)
    {
        // Arrange
        var validateTransactionDetailsRequest = new ValidateTransactionDetailsRequest(
            user,
            DescriptionTestData.EmptyDescription,
            Category.None.Value,
            -1.0m,
            currency.Value,
            default,
            TransactionType.Expense.Value);

        var transactionDetailsValidator = new TransactionDetailsValidator();

        // Act
        Result result = transactionDetailsValidator.Validate(validateTransactionDetailsRequest);

        // Assert
        result.Error.Should().Be(DomainErrors.User.CurrencyDoesNotExist);
    }

    [Theory]
    [ClassData(typeof(ValidateTransactionDetailsWithInvalidAmountForTransactionTypeData))]
    public void Validate_ShouldReturnFailureResult_WhenAmountIsInvalidForTransactionType(
        ValidateTransactionDetailsRequest validateTransactionDetailsRequest,
        Error expectedError)
    {
        // Arrange
        var transactionDetailsValidator = new TransactionDetailsValidator();

        // Act
        Result result = transactionDetailsValidator.Validate(validateTransactionDetailsRequest);

        // Assert
        result.Error.Should().Be(expectedError);
    }

    [Theory]
    [ClassData(typeof(CreateTransactionValidData))]
    public void Validate_ShouldReturnSuccessResult_WhenArgumentsAreValid(CreateTransactionRequest createTransactionRequest)
    {
        // Arrange
        var validateTransactionDetailsRequest = createTransactionRequest.ToValidateTransactionDetailsRequest();

        var transactionDetailsValidator = new TransactionDetailsValidator();

        // Act
        Result result = transactionDetailsValidator.Validate(validateTransactionDetailsRequest);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(CreateTransactionValidData))]
    public void Validate_ShouldReturnTransactionDetailsWithProperValues_WhenArgumentsAreValid(
        CreateTransactionRequest createTransactionRequest)
    {
        // Arrange
        var validateTransactionDetailsRequest = createTransactionRequest.ToValidateTransactionDetailsRequest();

        var transactionDetailsValidator = new TransactionDetailsValidator();

        // Act
        Result<ITransactionDetails> result = transactionDetailsValidator.Validate(validateTransactionDetailsRequest);

        // Assert
        result.Value.Description.Should().Be(Description.Create(validateTransactionDetailsRequest.Description).Value);
        result.Value.Category.Should().Be(Category.FromValue(validateTransactionDetailsRequest.Category).Value);
        result.Value.Money.Should().Be(new Money(createTransactionRequest.Amount, Currency.FromValue(createTransactionRequest.Currency).Value));
        result.Value.OccurredOn.Should().Be(validateTransactionDetailsRequest.OccurredOn);
        result.Value.TransactionType.Should().Be(TransactionType.FromValue(validateTransactionDetailsRequest.TransactionType).Value);
    }
}
