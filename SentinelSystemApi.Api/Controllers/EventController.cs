using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Models.Filters;
using SentinelSystemApi.Api.Services;

namespace SentinelSystemApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class EventController : ControllerBase
{
    private readonly IEventService _eventService;

    public EventController(IEventService eventService)
    {
        _eventService = eventService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<EventResponseDto>>> GetAll([FromQuery] EventFilterParams filterParams)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _eventService.GetAllEvents(filterParams, userId, isAdmin);
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<EventResponseDto>> GetById(int id)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _eventService.GetEventById(id, userId, isAdmin);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<EventResponseDto>> Update(int id, UpdateEventRequestDto requestDto)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _eventService.UpdateEvent(id, requestDto, userId, isAdmin);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int id)
    {
        await _eventService.DeleteEvent(id);
        return NoContent();
    }

    private (int userId, bool isAdmin) GetCurrentUser()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Admin");
        return (int.Parse(idClaim!), isAdmin);
    }
}