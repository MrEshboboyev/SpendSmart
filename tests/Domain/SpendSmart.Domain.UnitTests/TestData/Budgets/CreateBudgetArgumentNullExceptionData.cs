using SpendSmart.Domain.Modules.Budgets.Contracts;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Names;
using SpendSmart.Domain.UnitTests.TestData.Users;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Budgets;

public class CreateBudgetArgumentNullExceptionData : TheoryData<User, IBudgetDetails, string>
{
    public CreateBudgetArgumentNullExceptionData()
    {
        Add(null, new BudgetDetails(), "user");

        User user = UserTestData.ValidUser;

        Add(
            user,
            new BudgetDetails
            {
                Name = NameTestData.ValidName,
                Money = null
            },
            "money");

        Add(
            user,
            new BudgetDetails
            {
                Name = NameTestData.ValidName,
                Money = Money.Zero(CurrencyTestData.DefaultCurrency),
                Categories = null
            },
            "categories");
    }
}
