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

public class UserAndTransactionDetailsValidData : TheoryData<User, ITransactionDetails>
{
    public UserAndTransactionDetailsValidData()
    {
        User user = UserTestData.ValidUser;

        Currency currency = CurrencyTestData.DefaultCurrency;

        user.AddCurrency(currency);

        var expenseTransactionDetails = new TransactionDetails
        {
            Description = DescriptionTestData.EmptyDescription,
            Category = Category.None,
            Money = new Money(-1, currency),
            OccurredOn = DateTime.UtcNow.Date,
            TransactionType = TransactionType.Expense
        };

        Add(user, expenseTransactionDetails);

        var incomeTransactionDetails = new TransactionDetails
        {
            Description = DescriptionTestData.EmptyDescription,
            Category = Category.None,
            Money = new Money(1, currency),
            OccurredOn = DateTime.UtcNow.Date,
            TransactionType = TransactionType.Income
        };

        Add(user, incomeTransactionDetails);
    }
}
