using SpendSmart.Domain.Modules.Common;
using SpendSmart.Domain.Modules.Users;
using SpendSmart.Domain.UnitTests.TestData.Currencies;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Users;

public class UserWithNoCurrencyData : TheoryData<User, Currency>
{
    public UserWithNoCurrencyData() => Add(UserTestData.ValidUser, CurrencyTestData.DefaultCurrency);
}
