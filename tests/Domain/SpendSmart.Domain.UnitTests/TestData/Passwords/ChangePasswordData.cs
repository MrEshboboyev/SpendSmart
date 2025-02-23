using SpendSmart.Domain.Modules.Users;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Passwords;

public class ChangePasswordData : TheoryData<Password, Password>
{
    public ChangePasswordData() => Add(Password.Create("123aA!").Value, Password.Create("123aA!!").Value);
}
