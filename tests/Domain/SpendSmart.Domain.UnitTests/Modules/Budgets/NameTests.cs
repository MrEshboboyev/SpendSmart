using FluentAssertions;
using SpendSmart.Common.Primitives.Result;
using SpendSmart.Domain.Errors;
using SpendSmart.Domain.Modules.Budgets;
using SpendSmart.Domain.UnitTests.TestData.Names;
using Xunit;

namespace SpendSmart.Domain.UnitTests.Modules.Budgets;

public class NameTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Create_ShouldReturnFailureResult_WhenNameIsNullOrEmpty(string name)
    {
        // Arrange
        // Act
        Result<Name> result = Name.Create(name);

        // Assert
        result.Error.Should().Be(DomainErrors.Name.NullOrEmpty);
    }

    [Fact]
    public void Create_ShouldReturnFailureResult_WhenDescriptionIsTooLong()
    {
        // Arrange
        string description = NameTestData.LongerThanAllowedName;

        // Act
        Result<Name> result = Name.Create(description);

        // Assert
        result.Error.Should().Be(DomainErrors.Name.LongerThanAllowed);
    }
}
