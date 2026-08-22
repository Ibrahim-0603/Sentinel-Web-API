using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Exceptions;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;
using SentinelSystemApi.Api.Repositories;

namespace SentinelSystemApi.Api.Services;

public class TelemetryService : ITelemetryService
{
	private readonly ITelemetryRepository _telemetryRepository;
	private readonly IMapper _mapper;

	public TelemetryService(ITelemetryRepository telemetryRepository, IMapper mapper)
	{
		_telemetryRepository = telemetryRepository;
		_mapper = mapper;
	}

	public async Task<PagedResult<TelemetryResponseDto>> GetAllTelemetry(TelemetryFilterParams filterParams)
	{
		var result = await _telemetryRepository.Query(filterParams);
		return new PagedResult<TelemetryResponseDto>
		{
			Data = _mapper.Map<IEnumerable<TelemetryResponseDto>>(result.Data),
			Page = result.Page,
			PageSize = result.PageSize,
			TotalCount = result.TotalCount
		};
	}

	public async Task<TelemetryResponseDto> GetTelemetryById(int id)
	{
		var telemetry = await _telemetryRepository.GetById(id);
		if (telemetry is null) throw new NotFoundException(id, "Telemetry");
		return _mapper.Map<TelemetryResponseDto>(telemetry);
	}

	public async Task<TelemetryResponseDto> AddTelemetry(TelemetryRequestDto requestDto)
	{
		var telemetry = _mapper.Map<Telemetry>(requestDto);
		var created = await _telemetryRepository.AddTelemetry(telemetry);
		return _mapper.Map<TelemetryResponseDto>(created);

	}

	public async Task DeleteTelemetry(int id)
	{
		var telemetry = await _telemetryRepository.GetById(id);
		if (telemetry is null) throw new NotFoundException(id, "Telemetry");
		await _telemetryRepository.DeleteTelemetry(telemetry);
	}
}