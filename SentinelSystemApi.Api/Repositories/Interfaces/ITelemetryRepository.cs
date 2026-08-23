using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Repositories;

public interface ITelemetryRepository
{
	Task<PagedResult<Telemetry>> Query(TelemetryFilterParams filterParams);
	Task<Telemetry?> GetById(int id);
	Task<Telemetry> AddTelemetry(Telemetry telemetry);
	Task DeleteTelemetry(Telemetry telemetry);
	Task<Telemetry?> GetLatestReadingByDeviceId(int deviceId, int? excludedId = null);
}