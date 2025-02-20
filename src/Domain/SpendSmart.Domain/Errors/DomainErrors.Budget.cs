using SpendSmart.Common.Primitives.Errors;
using System;

namespace SpendSmart.Domain.Errors;

/// <summary>
/// Contains the domain errors.
/// </summary>
public static partial class DomainErrors
{
    /// <summary>
    /// Contains the budget errors.
    /// </summary>
    public static class Budget
    {
        /// <summary>
        /// Gets the budget end date precedes the start date error.
        /// </summary>
        public static Func<DateTime, DateTime, Error> EndDatePrecedesStartDate => (startDate, endDate) =>
            new Error(
                "Budget.EndDatePrecedesStartDate", 
                $"The end date {endDate:d} precedes the start date {startDate:d}.");
    }
}
