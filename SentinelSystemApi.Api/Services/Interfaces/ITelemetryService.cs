using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Services;

public interface ITelemetryService
{
	Task<PagedResult<TelemetryResponseDto>> GetAllTelemetry(TelemetryFilterParams filterParams);
	Task<TelemetryResponseDto> GetTelemetryById(int id);
	Task<TelemetryResponseDto> AddTelemetry(TelemetryRequestDto requestDto);
	Task DeleteTelemetry(int id);

}