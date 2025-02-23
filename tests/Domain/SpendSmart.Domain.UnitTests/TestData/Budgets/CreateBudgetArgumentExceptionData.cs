using System;
using SpendSmart.Domain.Modules.Budgets.Contracts;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Names;
using SpendSmart.Domain.UnitTests.TestData.Users;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Budgets;

public class CreateBudgetArgumentExceptionData : TheoryData<User, IBudgetDetails, string>
{
    public CreateBudgetArgumentExceptionData()
    {
        User user = UserTestData.ValidUser;

        Add(
            user,
            new BudgetDetails
            {
                Name = null
            },
            "name");

        Add(
            user,
            new BudgetDetails
            {
                Name = NameTestData.ValidName,
                Money = Money.Zero(CurrencyTestData.DefaultCurrency),
                Categories = Array.Empty<Category>(),
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow
            },
            "money");

        Add(
            user,
            new BudgetDetails
            {
                Name = NameTestData.ValidName,
                Money = Money.Zero(CurrencyTestData.DefaultCurrency),
                Categories = Array.Empty<Category>(),
                StartDate = default
            },
            "startDate");

        Add(
            user,
            new BudgetDetails
            {
                Name = NameTestData.ValidName,
                Money = Money.Zero(CurrencyTestData.DefaultCurrency),
                Categories = Array.Empty<Category>(),
                StartDate = DateTime.UtcNow,
                EndDate = default
            },
            "endDate");
    }
}
