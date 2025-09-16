using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Domain.Repositories;
using GameRouletteBackend.Shared.Infrastructure.Persistence.EFC.Configuration;
using Microsoft.EntityFrameworkCore;

namespace GameRouletteBackend.Roulette.Infrastructure.Persistence.EFC.Repositories;

public class RouletteGameRepository(AppDbContext context) : IRouletteGameRepository
{
    private readonly AppDbContext _context = context;

    public async Task<RouletteGame?> FindByGameIdAsync(Guid gameId)
    {
        return await _context.Set<RouletteGame>()
            .FirstOrDefaultAsync(g => g.GameId == gameId);
    }

    public async Task AddAsync(RouletteGame game)
    {
        await _context.Set<RouletteGame>().AddAsync(game);
    }

    public void Update(RouletteGame game)
    {
        _context.Set<RouletteGame>().Update(game);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}
