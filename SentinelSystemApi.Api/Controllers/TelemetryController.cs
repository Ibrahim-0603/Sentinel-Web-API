using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models.Filters;
using SentinelSystemApi.Api.Services;

namespace SentinelSystemApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[DisableRateLimiting]
public class TelemetryController : ControllerBase
{
	private readonly ITelemetryService _telemetryService;

	public TelemetryController(ITelemetryService telemetryService)
	{
		_telemetryService = telemetryService;
	}

	[HttpGet]
	[Authorize]
	public async Task<ActionResult<PagedResult<TelemetryResponseDto>>> GetAll([FromQuery] TelemetryFilterParams filterParams)
	{
		var (userId, isAdmin) = GetCurrentUser();
		var result = await _telemetryService.GetAllTelemetry(filterParams, userId, isAdmin);
		return Ok(result);
	}

	[HttpGet("{id}")]
	[Authorize]
	public async Task<ActionResult<TelemetryResponseDto>> GetById(int id)
	{
		var (userId, isAdmin) = GetCurrentUser();
		var result = await _telemetryService.GetTelemetryById(id, userId, isAdmin);
		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult<TelemetryResponseDto>> Create(TelemetryRequestDto requestDto)
	{
		var created = await _telemetryService.AddTelemetry(requestDto);
		return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
	}
	[HttpDelete("{id}")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Delete(int id)
	{
		await _telemetryService.DeleteTelemetry(id);
		return NoContent();
	}

	private (int userId, bool isAdmin) GetCurrentUser()
	{
		var idClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
		var isAdmin = User.IsInRole("Admin");
		return (int.Parse(idClaim!), isAdmin);
	}
}