using System.Collections.Generic;
using SpendSmart.Application.Contracts.TimeZones;
using SpendSmart.Common.Abstractions.Messaging;

namespace SpendSmart.Application.Queries.TimeZones;

/// <summary>
/// Represents the query for getting the collection of supported time zones.
/// </summary>
public sealed class GetTimeZonesQuery : IQuery<IEnumerable<TimeZoneResponse>>
{
}
