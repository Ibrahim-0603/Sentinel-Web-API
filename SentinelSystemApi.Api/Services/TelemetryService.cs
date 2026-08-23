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
	private readonly IDeviceRepository _deviceRepository;
	private readonly IEventRepository _eventRepository;
	private readonly IMapper _mapper;

	public TelemetryService(ITelemetryRepository telemetryRepository, IDeviceRepository deviceRepository, IEventRepository eventRepository, IMapper mapper)
	{
		_telemetryRepository = telemetryRepository;
		_deviceRepository = deviceRepository;
		_eventRepository = eventRepository;
		_mapper = mapper;
	}

	public async Task<PagedResult<TelemetryResponseDto>> GetAllTelemetry(TelemetryFilterParams filterParams, int callerId, bool isAdmin)
	{
		if (!isAdmin) filterParams.OwnerId = callerId;
		var result = await _telemetryRepository.Query(filterParams);

		return new PagedResult<TelemetryResponseDto>
		{
			Data = _mapper.Map<IEnumerable<TelemetryResponseDto>>(result.Data),
			Page = result.Page,
			PageSize = result.PageSize,
			TotalCount = result.TotalCount
		};
	}

	public async Task<TelemetryResponseDto> GetTelemetryById(int id, int callerId, bool isAdmin)
	{
		var telemetry = await _telemetryRepository.GetById(id);
		if (telemetry is null) throw new NotFoundException(id, "Telemetry");
		if (!isAdmin && telemetry.Device.OwnerId != callerId) throw new ForbiddenException("You do not have access to this telemetry reading.");
		return _mapper.Map<TelemetryResponseDto>(telemetry);
	}

	public async Task<TelemetryResponseDto> AddTelemetry(TelemetryRequestDto requestDto)
	{
		var deviceExists = await _deviceRepository.GetById(requestDto.DeviceId);
		if (deviceExists is null) throw new NotFoundException(requestDto.DeviceId, "Device");

		var telemetry = _mapper.Map<Telemetry>(requestDto);
		var created = await _telemetryRepository.AddTelemetry(telemetry);

		if (created.Pir)
		{
			var prevReading = await _telemetryRepository.GetLatestReadingByDeviceId(created.DeviceId, created.Id);
			bool prevPir = prevReading.Pir;
			if (!prevPir && created.Pir)
			{
				await _eventRepository.Create(new Event
				{
					EventType = Enums.EventType.MotionDetected,
					Timestamp = created.Timestamp,
					TelemetryId = created.Id
				});
			}
		}

		return _mapper.Map<TelemetryResponseDto>(created);

	}

	public async Task DeleteTelemetry(int id)
	{
		var telemetry = await _telemetryRepository.GetById(id);
		if (telemetry is null) throw new NotFoundException(id, "Telemetry");
		await _telemetryRepository.DeleteTelemetry(telemetry);
	}
}