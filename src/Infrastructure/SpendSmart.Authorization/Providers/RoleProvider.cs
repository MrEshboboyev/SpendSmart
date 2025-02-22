using SpendSmart.Authorization.Abstractions;
using SpendSmart.Common.Primitives.ServiceLifetimes;
using SpendSmart.Domain.Modules.Users;
using System.Collections.Generic;

namespace SpendSmart.Authorization.Providers;

/// <summary>
/// Represents the role provider.
/// </summary>
internal sealed class RoleProvider : IRoleProvider, ITransient
{
    /// <inheritdoc />
    public IEnumerable<string> GetStandardRoles()
    {
        yield return Role.User.Name;
    }
}
