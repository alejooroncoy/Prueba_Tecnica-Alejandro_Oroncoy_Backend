using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Domain.Model.Queries;

namespace GameRouletteBackend.Roulette.Domain.Services;

public interface IRouletteQueryService
{
    Task<RouletteGame?> Handle(GetGameByIdQuery query);
    Task<IEnumerable<Bet>> Handle(GetAllBetsByGameQuery query);
}
