using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Services;

public interface ITelemetryService
{
	Task<PagedResult<TelemetryResponseDto>> GetAllTelemetry(TelemetryFilterParams filterParams, int callerId, bool isAdmin);
	Task<TelemetryResponseDto> GetTelemetryById(int id, int callerId, bool isAdmin);
	Task<TelemetryResponseDto> AddTelemetry(TelemetryRequestDto requestDto);
	Task DeleteTelemetry(int id);

}