using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Repositories;
using GameRouletteBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GameRouletteBackend.IAM.Infrastructure.Persistence.EFC.Repositories;

public class UserRepository(AppDbContext context) : IUserRepository
{
    private readonly AppDbContext _context = context;

    public async Task<User?> FindByUidAsync(Guid uid)
    {
        return await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Uid == uid);
    }

    public async Task<User?> FindByNameAsync(string name)
    {
        var normalizedName = name.ToLowerInvariant();
        return await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.Name == normalizedName);
    }

    public async Task<User?> FindByAccountUidAsync(Guid accountUid)
    {
        return await _context.Set<User>()
            .FirstOrDefaultAsync(u => u.AccountUid == accountUid);
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Set<User>()
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.Set<User>().AddAsync(user);
    }

    public void Update(User user)
    {
        _context.Set<User>().Update(user);
    }

    public void Remove(User user)
    {
        _context.Set<User>().Remove(user);
    }
}
