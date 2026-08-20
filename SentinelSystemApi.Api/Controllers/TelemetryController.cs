using Microsoft.AspNetCore.Mvc;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models.Filters;
using SentinelSystemApi.Api.Repositories;
using SentinelSystemApi.Api.Services;

namespace SentinelSystemApi.Api.Controllers;

[ApiController]
[Route("api/[controller]")]

public class TelemetryController : ControllerBase
{
	private readonly ITelemetryService _telemetryService;

	public TelemetryController(ITelemetryService telemetryService)
	{
		_telemetryService = telemetryService;
	}

	[HttpGet]
	public async Task<ActionResult<PagedResult<TelemetryResponseDto>>> GetAll([FromQuery] TelemetryFilterParams filterParams)
	{
		var result = await _telemetryService.GetAllTelemetry(filterParams);
		return Ok(result);
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<TelemetryResponseDto>> GetById(int id)
	{
		var result = await _telemetryService.GetTelemetryById(id);
		return Ok(result);
	}

	[HttpPost]
	public async Task<ActionResult<TelemetryResponseDto>> Create(TelemetryRequestDto requestDto)
	{
		var created = await _telemetryService.AddTelemetry(requestDto);
		return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
	}
	[HttpDelete("{id}")]
	public async Task<ActionResult> Delete(int id)
	{
		await _telemetryService.DeleteTelemetry(id);
		return NoContent();
	}
}