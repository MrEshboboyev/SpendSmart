using SpendSmart.Domain.Modules.Users;
using System;

namespace SpendSmart.Domain.Modules.Budgets.Contracts;

/// <summary>
/// Represents the request for validating budget details.
/// </summary>
public sealed record ValidateBudgetDetailsRequest(
    User User,
    string Name,
    int[] Categories,
    decimal Amount,
    int Currency,
    DateTime StartDate,
    DateTime EndDate);
