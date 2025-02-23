using System;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Domain.Modules.Transactions.Contracts;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Descriptions;
using SpendSmart.Domain.UnitTests.TestData.Users;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Transactions;

public class CreateTransactionInvalidAmountForTransactionTypeData : TheoryData<User, ITransactionDetails>
{
    public CreateTransactionInvalidAmountForTransactionTypeData()
    {
        User user = UserTestData.ValidUser;

        Add(user, new TransactionDetails
        {
            Description = DescriptionTestData.EmptyDescription,
            Category = Category.None,
            Money = new Money(1, CurrencyTestData.DefaultCurrency),
            OccurredOn = DateTime.UtcNow,
            TransactionType = TransactionType.Expense
        });

        Add(user, new TransactionDetails
        {
            Description = DescriptionTestData.EmptyDescription,
            Category = Category.None,
            Money = new Money(-1, CurrencyTestData.DefaultCurrency),
            OccurredOn = DateTime.UtcNow,
            TransactionType = TransactionType.Income
        });
    }
}
