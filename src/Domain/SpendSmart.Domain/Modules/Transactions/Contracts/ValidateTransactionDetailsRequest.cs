using SpendSmart.Domain.Modules.Users;
using System;

namespace SpendSmart.Domain.Modules.Transactions.Contracts;

/// <summary>
/// Represents the request for validating transaction details.
/// </summary>
public sealed record ValidateTransactionDetailsRequest(
    User User,
    string Description,
    int Category,
    decimal Amount,
    int Currency,
    DateTime OccurredOn,
    int TransactionType);
