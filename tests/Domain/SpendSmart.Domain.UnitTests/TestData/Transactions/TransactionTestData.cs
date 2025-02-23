using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Domain.Modules.Transactions.Contracts;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Descriptions;
using SpendSmart.Domain.UnitTests.TestData.Users;
using System;

namespace SpendSmart.Domain.UnitTests.TestData.Transactions;

public static class TransactionTestData
{
    public static Transaction ValidExpense
    {
        get
        {
            User user = UserTestData.ValidUser;

            user.AddCurrency(CurrencyTestData.DefaultCurrency);

            var transactionDetails = new TransactionDetails
            {
                Description = DescriptionTestData.EmptyDescription,
                Category = Category.Bills,
                Money = new Money(-1.0m, CurrencyTestData.DefaultCurrency),
                OccurredOn = DateTime.UtcNow.Date,
                TransactionType = TransactionType.Expense
            };

            return Transaction.Create(user, transactionDetails);
        }
    }
}
