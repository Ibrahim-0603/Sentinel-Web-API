using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Services;

public interface ITokenService
{
      string GenerateToken(User user);
}