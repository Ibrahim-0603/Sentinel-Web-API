using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Exceptions;
using SentinelSystemApi.Api.Models.Filters;
using SentinelSystemApi.Api.Repositories;

namespace SentinelSystemApi.Api.Services;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;
    private readonly IMapper _mapper;

    public EventService(IEventRepository eventRepository, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _mapper = mapper;
    }

    public async Task<PagedResult<EventResponseDto>> GetAllEvents(EventFilterParams filterParams, int callerId, bool isAdmin)
    {
        if (!isAdmin) filterParams.OwnerId = callerId;
        var result = await _eventRepository.Query(filterParams);
        return new PagedResult<EventResponseDto>
        {
            Data = _mapper.Map<IEnumerable<EventResponseDto>>(result.Data),
            Page = result.Page,
            PageSize = result.PageSize,
            TotalCount = result.TotalCount
        };
    }

    public async Task<EventResponseDto?> GetEventById(int id, int callerId, bool isAdmin)
    {
        var evnt = await _eventRepository.GetById(id);
        if (evnt is null) throw new NotFoundException(id, "Event");
        if (!isAdmin && evnt.Telemetry?.Device.OwnerId != callerId) throw new ForbiddenException("You do not have access to this event");
        return _mapper.Map<EventResponseDto>(evnt);
    }

    public async Task<EventResponseDto> UpdateEvent(int id, UpdateEventRequestDto requestDto, int callerId, bool isAdmin)
    {
        var evnt = await _eventRepository.GetById(id);
        if (evnt is null) throw new NotFoundException(id, "Event");
        if (!isAdmin && evnt.Telemetry?.Device.OwnerId != callerId) throw new ForbiddenException("You do not have access to this event");
        evnt.Notes = requestDto.Notes;
        var updated = await _eventRepository.Update(evnt);
        return _mapper.Map<EventResponseDto>(updated);
    }
    public async Task DeleteEvent(int id)
    {
        var evnt = await _eventRepository.GetById(id);
        if (evnt is null) throw new NotFoundException(id, "Event");
        await _eventRepository.Delete(evnt);
    }
}