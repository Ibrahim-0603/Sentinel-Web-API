using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Services;

namespace SentinelSystemApi.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
	private readonly IAuthService _authService;

	public UserController(IAuthService authService)
	{
		_authService = authService;
	}

	[HttpGet]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<IEnumerable<UserResponseDto>>> GetAll()
	{
		var users = await _authService.GetAllUsers();
		return Ok(users);
	}

	[HttpGet("id")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<UserResponseDto>> GetById(int id)
	{
		var user = await _authService.GetUserById(id);
		return Ok(user);
	}

	[HttpGet("username")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<UserResponseDto>> GetByUsername(string username)
	{
		var user = await _authService.GetUserByUsername(username);
		return Ok(user);
	}

	[HttpPut("id")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult<UserResponseDto>> Update(int id, UpdateUserRequestDto requestDto)
	{
		var updated = await _authService.UpdateUser(id, requestDto);
		return Ok(updated);
	}

	[HttpDelete("id")]
	[Authorize(Roles = "Admin")]
	public async Task<ActionResult> Delete(int id)
	{
		await _authService.DeleteUser(id);
		return NoContent();
	}
}