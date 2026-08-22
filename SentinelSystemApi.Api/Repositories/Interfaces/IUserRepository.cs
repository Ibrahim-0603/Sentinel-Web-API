using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Repositories;

public interface IUserRepository
{
	Task<IEnumerable<User>> GetAll();
	Task<User?> GetById(int id);
	Task<User?> GetByUsername(string username);
	Task<User> Create(User user);
	Task<User> Update(User user);
	Task Delete(User user);

}