using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Services;

namespace SentinelSystemApi.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class DeviceStatusController : ControllerBase
{
    private readonly IDeviceStatusService _deviceStatusService;

    public DeviceStatusController(IDeviceStatusService deviceStatusService)
    {
        _deviceStatusService = deviceStatusService;
    }

    [HttpGet("device/{deviceId:int}")]
    public async Task<ActionResult<DeviceStatusResponseDto>> GetByDeviceId(int deviceId)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _deviceStatusService.GetByDeviceId(deviceId, userId, isAdmin);
        return Ok(result);
    }

    [HttpGet("device/name/{deviceName}")]
    public async Task<ActionResult<DeviceStatusResponseDto>> GetByDeviceName(string deviceName)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _deviceStatusService.GetByDeviceName(deviceName, userId, isAdmin);
        return Ok(result);
    }

    [HttpPut("device/{deviceId:int}")]
    public async Task<ActionResult<DeviceStatusResponseDto>> UpdateById(int deviceId, UpdateDeviceStatusRequestDto requestDto)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _deviceStatusService.UpdateStatusById(deviceId, requestDto, userId, isAdmin);
        return Ok(result);
    }
    [HttpPut("device/name/{deviceName}")]
    public async Task<ActionResult<DeviceStatusResponseDto>> UpdateNyName(string deviceName, UpdateDeviceStatusRequestDto requestDto)
    {
        var (userId, isAdmin) = GetCurrentUser();
        var result = await _deviceStatusService.UpdateStatusByName(deviceName, requestDto, userId, isAdmin);
        return Ok(result);
    }
    [HttpDelete("device/{deviceId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Delete(int deviceId)
    {
        await _deviceStatusService.DeleteStatus(deviceId);
        return NoContent();
    }

    private (int userId, bool isAdmin) GetCurrentUser()
    {
        var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var isAdmin = User.IsInRole("Admin");
        return (int.Parse(idClaim!), isAdmin);
    }
}