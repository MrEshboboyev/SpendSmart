using SpendSmart.Domain.Modules.Users.Contracts;
using Xunit;

namespace SpendSmart.Domain.UnitTests.TestData.Users;

public class CreateUserWithRolesValidData : TheoryData<CreateUserRequest, string[]>
{
    public CreateUserWithRolesValidData() =>
        Add(
            new CreateUserRequest(UserTestData.FirstName, UserTestData.LastName, UserTestData.Email, UserTestData.Password),
            ["Role1", "Role2"]);
}
