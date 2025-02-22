using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SpendSmart.Application.Contracts.TimeZones;
using SpendSmart.Application.Queries.TimeZones;
using SpendSmart.Common.Abstractions.Messaging;
using TimeZoneConverter;

namespace SpendSmart.Application.Queries.Handlers.TimeZones;

/// <summary>
/// Represents the <see cref="GetTimeZonesQuery"/> handler.
/// </summary>
internal sealed class GetTimeZonesQueryHandlers : IQueryHandler<GetTimeZonesQuery, IEnumerable<TimeZoneResponse>>
{
    /// <inheritdoc />
    public Task<IEnumerable<TimeZoneResponse>> Handle(GetTimeZonesQuery request, CancellationToken cancellationToken)
    {
        IEnumerable<TimeZoneResponse> timeZones = TZConvert.KnownWindowsTimeZoneIds
            .Select(TZConvert.GetTimeZoneInfo)
            .GroupBy(timeZoneInfo => new { timeZoneInfo.DisplayName, timeZoneInfo.SupportsDaylightSavingTime })
            .Select(timeZoneInfoGroup => timeZoneInfoGroup.First())
            .OrderBy(timeZoneInfo => timeZoneInfo.BaseUtcOffset.Hours)
            .Select(timeZoneInfo => new TimeZoneResponse(timeZoneInfo.Id, timeZoneInfo.DisplayName));

        return Task.FromResult(timeZones);
    }
}
