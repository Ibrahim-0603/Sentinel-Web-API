using AutoMapper;
using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Enums;
using SentinelSystemApi.Api.Exceptions;
using SentinelSystemApi.Api.Models;
using SentinelSystemApi.Api.Repositories;

namespace SentinelSystemApi.Api.Services;

public class AuthService : IAuthService
{
	private readonly IUserRepository _userRepository;
	private readonly IMapper _mapper;
	private readonly ITokenService _tokenService;
	public AuthService(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
	{
		_userRepository = userRepository;
		_mapper = mapper;
		_tokenService = tokenService;
	}

	public async Task<IEnumerable<UserResponseDto>> GetAllUsers()
	{
		var users = await _userRepository.GetAll();
		return _mapper.Map<IEnumerable<UserResponseDto>>(users);
	}

	public async Task<UserResponseDto?> GetUserById(int id)
	{
		var user = await _userRepository.GetById(id);
		if (user is null) throw new NotFoundException(id, "User");
		return _mapper.Map<UserResponseDto>(user);
	}
	public async Task<UserResponseDto?> GetUserByUsername(string username)
	{
		var user = await _userRepository.GetByUsername(username);
		if (user is null) throw new NotFoundException(username, "User");
		return _mapper.Map<UserResponseDto>(user);
	}

	public async Task<UserResponseDto> RegisterUser(RegisterRequestDto requestDto)
	{
		var user = new User
		{
			Username = requestDto.Username,
			PasswordHash = BCrypt.Net.BCrypt.HashPassword(requestDto.Password),
			Role = UserRole.User,
			CreatedAt = DateTime.UtcNow
		};
		await _userRepository.Create(user);
		return _mapper.Map<UserResponseDto>(user);
	}

	public async Task<UserResponseDto> UpdateUser(int id, UpdateUserRequestDto requestDto)
	{
		var user = await _userRepository.GetById(id);
		if (user is null) throw new NotFoundException(id, "User");

		if (requestDto.Username is not null) user.Username = requestDto.Username;
		if (requestDto.Role is not null) user.Role = requestDto.Role.Value;

		var updated = await _userRepository.Update(user);
		return _mapper.Map<UserResponseDto>(updated);
	}

	public async Task DeleteUser(int id)
	{
		var user = await _userRepository.GetById(id);
		if (user is null) throw new NotFoundException(id, "User");
		await _userRepository.Delete(user);
	}

	public async Task<string> Login(LoginRequestDto requestDto)
	{
		var user  = await _userRepository.GetByUsername(requestDto.Username);
		if(user is null || !BCrypt.Net.BCrypt.Verify(requestDto.Password, user.PasswordHash)) throw new UnauthorizedException("Wrong username or password.");
		return _tokenService.GenerateToken(user);
	}
}