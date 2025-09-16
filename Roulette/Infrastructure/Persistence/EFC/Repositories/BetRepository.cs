using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Domain.Repositories;
using GameRouletteBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Repositories;

public class BetRepository(AppDbContext context) : IBetRepository
{
    private readonly AppDbContext _context = context;

    public async Task<Bet?> FindByBetIdAsync(Guid betId)
    {
        return await _context.Set<Bet>()
            .FirstOrDefaultAsync(b => b.BetId == betId);
    }

    public async Task<IEnumerable<Bet>> FindByGameIdAsync(Guid gameId)
    {
        return await _context.Set<Bet>()
            .Where(b => b.GameId == gameId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Bet>> FindByUserUidAsync(Guid userUid)
    {
        return await _context.Set<Bet>()
            .Where(b => b.UserUid == userUid)
            .ToListAsync();
    }

    public async Task AddAsync(Bet bet)
    {
        await _context.Set<Bet>().AddAsync(bet);
    }

    public void Update(Bet bet)
    {
        _context.Set<Bet>().Update(bet);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
