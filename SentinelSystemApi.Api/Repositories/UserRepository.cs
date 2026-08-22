using Microsoft.EntityFrameworkCore;
using SentinelSystemApi.Api.Data;
using SentinelSystemApi.Api.Models;

namespace SentinelSystemApi.Api.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _context.Users.AsNoTracking().ToListAsync();
    }
    public async Task<User?> GetById(int id) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

    public async Task<User?> GetByUsername(string username) => await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Username == username);
    public async Task<User> Create(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }
    public async Task<User> Update(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task Delete(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}