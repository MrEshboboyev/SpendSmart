using SpendSmart.Common.Primitives.Errors;
using SpendSmart.Domain.Errors;
using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Transactions;
using SpendSmart.Domain.Modules.Transactions.Contracts;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using SpendSmart.Domain.UnitTests.TestData.Users;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Transactions;

public class ValidateTransactionDetailsWithInvalidAmountForTransactionTypeData : TheoryData<ValidateTransactionDetailsRequest, Error>
{
    public ValidateTransactionDetailsWithInvalidAmountForTransactionTypeData()
    {
        User user = UserTestData.ValidUser;

        Currency currency = CurrencyTestData.DefaultCurrency;

        Category category = Category.None;

        user.AddCurrency(currency);

        Add(
            new ValidateTransactionDetailsRequest(
                user,
                default,
                category.Value,
                0.0m,
                currency.Value,
                default,
                TransactionType.Expense.Value),
            DomainErrors.Transaction.ExpenseAmountGreaterThanOrEqualToZero);

        Add(
            new ValidateTransactionDetailsRequest(
                user,
                default,
                category.Value,
                1.0m,
                currency.Value,
                default,
                TransactionType.Expense.Value),
            DomainErrors.Transaction.ExpenseAmountGreaterThanOrEqualToZero);

        Add(
            new ValidateTransactionDetailsRequest(
                user,
                default,
                category.Value,
                0.0m,
                currency.Value,
                default,
                TransactionType.Income.Value),
            DomainErrors.Transaction.IncomeAmountLessThanOrEqualToZero);

        Add(
            new ValidateTransactionDetailsRequest(
                user,
                default,
                category.Value,
                -1.0m,
                currency.Value,
                default,
                TransactionType.Income.Value),
            DomainErrors.Transaction.IncomeAmountLessThanOrEqualToZero);
    }
}
