using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Domain.Modules.Transactions.Contracts;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Descriptions;
using SpendSmart.Domain.UnitTests.TestData.Users;
using System;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Transactions;

public class CreateTransactionValidData : TheoryData<CreateTransactionRequest>
{
    public CreateTransactionValidData()
    {
        User user = UserTestData.ValidUser;

        Currency currency = CurrencyTestData.DefaultCurrency;

        user.AddCurrency(currency);

        var createExpenseRequest = new CreateTransactionRequest(
            user,
            DescriptionTestData.EmptyDescription,
            Category.None.Value,
            -1.0m,
            currency.Value,
            DateTime.UtcNow.Date,
            TransactionType.Expense.Value);

        Add(createExpenseRequest);

        var createIncomeRequest = new CreateTransactionRequest(
            user,
            DescriptionTestData.EmptyDescription,
            Category.None.Value,
            1.0m,
            currency.Value,
            DateTime.UtcNow.Date,
            TransactionType.Income.Value);

        Add(createIncomeRequest);
    }
}
