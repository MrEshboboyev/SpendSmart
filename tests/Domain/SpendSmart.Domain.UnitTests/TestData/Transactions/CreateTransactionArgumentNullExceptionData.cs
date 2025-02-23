using System;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions.Contracts;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Descriptions;
using SpendSmart.Domain.UnitTests.TestData.Users;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Transactions;

public class CreateTransactionArgumentNullExceptionData : TheoryData<User, ITransactionDetails, string>
{
    public CreateTransactionArgumentNullExceptionData()
    {
        Add(null, new TransactionDetails(), "user");

        User user = UserTestData.ValidUser;

        Add(
            user,
            new TransactionDetails
            {
                Description = null
            },
            "description");

        Add(
            user,
            new TransactionDetails
            {
                Description = DescriptionTestData.EmptyDescription,
                Category = null
            },
            "category");

        Add(
            user,
            new TransactionDetails
            {
                Description = DescriptionTestData.EmptyDescription,
                Category = Category.None,
                Money = null
            },
            "money");

        Add(
            user,
            new TransactionDetails
            {
                Description = DescriptionTestData.EmptyDescription,
                Category = Category.None,
                Money = new Money(default, CurrencyTestData.DefaultCurrency),
                OccurredOn = DateTime.UtcNow,
                TransactionType = null
            },
            "transactionType");
    }
}
