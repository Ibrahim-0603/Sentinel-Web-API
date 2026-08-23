using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;

namespace SentinelSystemApi.Api.Services;

public interface IEventService
{
    Task<PagedResult<EventResponseDto>> GetAllEvents(EventFilterParams filterParams, int callerId, bool isAdmin);
    Task<EventResponseDto?> GetEventById(int id, int callerId, bool isAdmin);
    Task<EventResponseDto> UpdateEvent(int id, UpdateEventRequestDto requestDto, int callerId, bool isAdmin);
    Task DeleteEvent(int id);
}