﻿using System.Linq;
using SpendSmart.Domain.Modules.Users;
using Raven.Client.Documents.Indexes;

namespace SpendSmart.Persistence.Indexes.Users;

/// <summary>
/// Represents the index on users collection by refresh token field.
/// </summary>
public sealed class Users_ByRefreshToken : AbstractIndexCreationTask<User>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Users_ByRefreshToken"/> class.
    /// </summary>
    public Users_ByRefreshToken() =>
        Map = users =>
            from user in users
            select new
            {
                RefreshToken_Token = user.RefreshToken.Token
            };
}
