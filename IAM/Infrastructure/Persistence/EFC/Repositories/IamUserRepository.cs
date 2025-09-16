using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Repositories;
using GameRouletteBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GameRouletteBackend.IAM.Infrastructure.Persistence.EFC.Repositories;

public class AccountRepository(AppDbContext context) : IAccountRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Account?> FindByUidAsync(Guid uid)
    {
        return await _context.Set<Account>()
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Uid == uid);
    }

    public async Task<Account?> FindByNameAsync(string name)
    {
        var normalizedName = name.ToLowerInvariant();
        return await _context.Set<Account>()
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.Name == normalizedName);
    }

    public async Task<IEnumerable<Account>> GetAllAsync()
    {
        return await _context.Set<Account>()
            .Include(a => a.User)
            .ToListAsync();
    }

    public async Task AddAsync(Account account)
    {
        await _context.Set<Account>().AddAsync(account);
    }

    public void Update(Account account)
    {
        _context.Set<Account>().Update(account);
    }

    public void Remove(Account account)
    {
        _context.Set<Account>().Remove(account);
    }
}
