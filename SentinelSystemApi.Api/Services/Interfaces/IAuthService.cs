using SentinelSystemApi.Api.DTOs;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Services;

public interface IAuthService
{
	Task<IEnumerable<UserResponseDto>> GetAllUsers();
	Task<UserResponseDto?> GetUserById(int id);
	Task<UserResponseDto?> GetUserByUsername(string username);
	Task<UserResponseDto> RegisterUser(RegisterRequestDto requestDto);
	Task<UserResponseDto> UpdateUser(int id, UpdateUserRequestDto requestDto);
	Task DeleteUser(int id);
	Task<string> Login(LoginRequestDto requestDto);
}