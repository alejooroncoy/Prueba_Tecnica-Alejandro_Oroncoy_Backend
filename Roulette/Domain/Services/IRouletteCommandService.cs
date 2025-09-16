using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Domain.Model.Commands;

namespace GameRouletteBackend.Roulette.Domain.Services;

public interface IRouletteCommandService
{
    Task<RouletteGame> Handle(SpinRouletteCommand command);
    Task<Bet> Handle(PlaceBetCommand command);
    Task Handle(CalculateWinningsCommand command);
}
