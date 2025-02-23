using FluentAssertions;
using SpendSmart.Domain.Errors;
using SpendSmart.Domain.Modules.Budgets;
using SpendSmart.Domain.Modules.Budgets.Contracts;
using SpendSmart.Domain.Modules.Budgets.Exceptions;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Budgets;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Names;
using SpendSmart.Domain.UnitTests.TestData.Users;
using System;
using Xunit;

namespace SpendSmart.Domain.UnitTests.Modules.Budgets;

public class BudgetTests
{
    [Theory]
    [ClassData(typeof(CreateBudgetArgumentNullExceptionData))]
    public void Create_ShouldThrowArgumentNullException_WhenArgumentsAreInvalid(
        User user,
        IBudgetDetails budgetDetails,
        string paramName)
    {
        // Arrange
        // Act
        void action() => Budget.Create(user, budgetDetails);

        // Assert
        FluentActions.Invoking(action).Should().Throw<ArgumentNullException>().And
            .ParamName.Should().Be(paramName);
    }

    [Theory]
    [ClassData(typeof(CreateBudgetArgumentExceptionData))]
    public void Create_ShouldThrowArgumentException_WhenArgumentsAreInvalid(
        User user,
        IBudgetDetails budgetDetails,
        string paramName)
    {
        // Arrange
        // Act
        void action() => Budget.Create(user, budgetDetails);

        // Assert
        FluentActions.Invoking(action).Should().Throw<ArgumentException>().And
            .ParamName.Should().Be(paramName);
    }

    [Fact]
    public void Create_ShouldThrowBudgetEndDatePrecedesStartDateDomainException_WhenEndDatePrecedesStartDate()
    {
        // Arrange
        User user = UserTestData.ValidUser;

        var budgetDetails = new BudgetDetails
        {
            Name = NameTestData.ValidName,
            Categories = Array.Empty<Category>(),
            Money = new Money(1.0m, CurrencyTestData.DefaultCurrency),
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(-1)
        };

        // Act
        void action() => Budget.Create(user, budgetDetails);

        // Assert
        FluentActions.Invoking(action).Should().Throw<BudgetEndDatePrecedesStartDateDomainException>()
            .And.Error.Should().Be(DomainErrors.Budget.EndDatePrecedesStartDate(budgetDetails.StartDate, budgetDetails.EndDate));
    }

    [Fact]
    public void Create_ShouldCreateBudget_WithProperValues()
    {
        // Arrange
        User user = UserTestData.ValidUser;

        var budgetDetails = new BudgetDetails
        {
            Name = NameTestData.ValidName,
            Categories = [Category.Bills, Category.Clothing],
            Money = new Money(1.0m, CurrencyTestData.DefaultCurrency),
            StartDate = DateTime.UtcNow.Date,
            EndDate = DateTime.UtcNow.Date.AddDays(1)
        };

        // Act
        var budget = Budget.Create(user, budgetDetails);

        // Assert
        budget.UserId.Should().Be(Ulid.Parse(user.Id));
        budget.Name.Should().Be(budgetDetails.Name);
        budget.Categories.Should().Equal(budgetDetails.Categories);
        budget.Money.Should().Be(budgetDetails.Money);
        budget.StartDate.Should().Be(budgetDetails.StartDate);
        budget.EndDate.Should().Be(budgetDetails.EndDate);
        budget.Expired.Should().BeFalse();
        budget.CreatedOnUtc.Should().Be(default);
        budget.ModifiedOnUtc.Should().BeNull();
    }
}
