using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Domain.Modules.Transactions.Contracts;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Descriptions;
using SpendSmart.Domain.UnitTests.TestData.Users;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Transactions;

public class CreateTransactionArgumentExceptionData : TheoryData<User, ITransactionDetails, string>
{
    public CreateTransactionArgumentExceptionData() =>
        Add(
            UserTestData.ValidUser,
            new TransactionDetails
            {
                Description = DescriptionTestData.EmptyDescription,
                Category = Category.None,
                Money = new Money(default, CurrencyTestData.DefaultCurrency),
                OccurredOn = default,
                TransactionType = TransactionType.Expense
            },
            "occurredOn");
}
