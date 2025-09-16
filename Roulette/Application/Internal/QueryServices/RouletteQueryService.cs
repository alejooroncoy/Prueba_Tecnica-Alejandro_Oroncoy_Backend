using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Domain.Model.Queries;
using GameRouletteBackend.Roulette.Domain.Repositories;
using GameRouletteBackend.Roulette.Domain.Services;

namespace GameRouletteBackend.Roulette.Application.Internal.QueryServices;

public class RouletteQueryService(
    IRouletteGameRepository gameRepository,
    IBetRepository betRepository)
    : IRouletteQueryService
{

    public async Task<RouletteGame?> Handle(GetGameByIdQuery query)
    {
        return await gameRepository.FindByGameIdAsync(query.GameId);
    }

    public async Task<IEnumerable<Bet>> Handle(GetAllBetsByGameQuery query)
    {
        return await betRepository.FindByGameIdAsync(query.GameId);
    }
}
