using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models.Filters;
using SentinelSystemApi.Api.Services;

namespace SentinelSystemApi.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]

public class DeviceController : ControllerBase
{
    private readonly IDeviceService _deviceService;

    public DeviceController(IDeviceService deviceService)
    {
        _deviceService = deviceService;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<DeviceResponseDto>>> GetAll([FromQuery] DeviceFilterParams filterParams)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _deviceService.GetAllDevices(filterParams, userId, isAdmin);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<DeviceResponseDto>> GetById(int id)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _deviceService.GetDeviceById(id, userId, isAdmin);
        return Ok(result);
    }

    [HttpGet("{name}")]
    public async Task<ActionResult<DeviceResponseDto>> GetByName(string name)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _deviceService.GetDeviceByName(name, userId, isAdmin);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<DeviceResponseDto>> Create([FromBody] DeviceRequestDto requestDto)
    {
        var (userId, _) = GetCurrentUser();
        var created = await _deviceService.CreateDevice(requestDto, userId);
        return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);

    }

    [HttpPut("{id:int}")]
    public async Task<ActionResult<DeviceResponseDto>> Update(int id, DeviceRequestDto requestDto)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var updated = await _deviceService.UpdateDevice(id, requestDto, userId, isAdmin);
        return Ok(updated);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        var (userId, isAdmin) = GetCurrentUser();
        await _deviceService.DeleteDevice(id, userId, isAdmin);
        return NoContent();
    }

    private (int userId, bool isAdmin) GetCurrentUser()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Admin");
        return (int.Parse(idClaim!), isAdmin);
    }

}