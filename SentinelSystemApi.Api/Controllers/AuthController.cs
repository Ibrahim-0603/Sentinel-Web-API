using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Services;

namespace SentinelSystemApi.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
	private readonly IAuthService _authService;
	private readonly IValidator<RegisterRequestDto> _registerValidator;

	public AuthController(IAuthService authService, IValidator<RegisterRequestDto> registerValidator)
	{
		_authService = authService;
		_registerValidator = registerValidator;
	}

	[HttpPost("register")]
	public async Task<ActionResult<UserResponseDto>> Register([FromBody] RegisterRequestDto requestDto)
	{
		var validationResult = await _registerValidator.ValidateAsync(requestDto);
		if(!validationResult.IsValid) throw new ValidationException(validationResult.Errors);
		
		var created = await _authService.RegisterUser(requestDto);
		return CreatedAtAction(nameof(UserController.GetById), "User", new { id = created.Id }, created);
	}

	[HttpPost("login")]
	public async Task<ActionResult<string>> Login([FromBody] LoginRequestDto requestDto)
	{
		var token = await _authService.Login(requestDto);
		return Ok(new { token });
	}
}