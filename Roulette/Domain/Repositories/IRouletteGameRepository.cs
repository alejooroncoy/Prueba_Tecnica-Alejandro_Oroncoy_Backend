using GameRouletteBackend.Roulette.Domain.Model.Aggregates;

namespace GameRouletteBackend.Roulette.Domain.Repositories;

public interface IRouletteGameRepository
{
    Task<RouletteGame?> FindByGameIdAsync(Guid gameId);
    Task AddAsync(RouletteGame game);
    void Update(RouletteGame game);
    Task SaveChangesAsync();
}
