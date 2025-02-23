using FluentAssertions;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Errors;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Domain.UnitTests.TestData.Categories;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using Xunit;

namespace SpendSmart.Domain.UnitTests.Modules.Transactions;

public class ExpenseTransactionTypeTests
{
    [Theory]
    [InlineData(1, "Expense")]
    public void ShouldHaveSpecifiedValueAndName(int value, string name)
    {
        // Arrange
        // Act
        TransactionType transactionType = TransactionType.Expense;

        // Assert
        transactionType.Value.Should().Be(value);
        transactionType.Name.Should().Be(name);
    }

    [Theory]
    [InlineData(-1)]
    public void ValidateAmount_ShouldReturnSuccessResult_WhenAmountIsLessThanZero(decimal amount)
    {
        // Arrange
        TransactionType transactionType = TransactionType.Expense;

        // Act
        Result result = transactionType.ValidateAmount(new Money(amount, CurrencyTestData.DefaultCurrency));

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1.0)]
    public void ValidateAmount_ShouldReturnFailureResult_WhenAmountIsGreaterThanOrEqualToZero(decimal amount)
    {
        // Arrange
        TransactionType transactionType = TransactionType.Expense;

        // Act
        Result result = transactionType.ValidateAmount(new Money(amount, CurrencyTestData.DefaultCurrency));

        // Assert
        result.Error.Should().Be(DomainErrors.Transaction.ExpenseAmountGreaterThanOrEqualToZero);
    }

    [Theory]
    [ClassData(typeof(IncomeCategoryData))]
    public void ValidateCategory_ShouldReturnFailureResult_WhenCategoryIsIncomeCategory(Category category)
    {
        // Arrange
        TransactionType transactionType = TransactionType.Expense;

        // Act
        Result result = transactionType.ValidateCategory(category);

        // Assert
        result.Error.Should().Be(DomainErrors.Transaction.ExpenseCategoryInvalid);
    }

    [Fact]
    public void ValidateCategory_ShouldReturnSuccess_WhenCategoryIsDefaultCategory()
    {
        // Arrange
        TransactionType transactionType = TransactionType.Expense;

        // Act
        Result result = transactionType.ValidateCategory(Category.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Theory]
    [ClassData(typeof(ExpenseCategoryData))]
    public void ValidateCategory_ShouldReturnSuccess_WhenCategoryIsExpenseCategory(Category category)
    {
        // Arrange
        TransactionType transactionType = TransactionType.Expense;

        // Act
        Result result = transactionType.ValidateCategory(category);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
}
