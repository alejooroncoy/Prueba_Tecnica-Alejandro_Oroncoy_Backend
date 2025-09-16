using GameRouletteBackend.Roulette.Domain.Model.Aggregates;

namespace GameRouletteBackend.Roulette.Domain.Repositories;

public interface IBetRepository
{
    Task<Bet?> FindByBetIdAsync(Guid betId);
    Task<IEnumerable<Bet>> FindByGameIdAsync(Guid gameId);
    Task<IEnumerable<Bet>> FindByUserUidAsync(Guid userUid);
    Task AddAsync(Bet bet);
    void Update(Bet bet);
    Task SaveChangesAsync();
}
