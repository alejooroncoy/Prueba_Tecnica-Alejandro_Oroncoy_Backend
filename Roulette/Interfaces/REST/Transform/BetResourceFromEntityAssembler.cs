using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Interfaces.REST.Resources;

namespace GameRouletteBackend.Roulette.Interfaces.REST.Transform;

public static class BetResourceFromEntityAssembler
{
    public static BetResource ToResourceFromEntity(Bet bet)
    {
        return new BetResource(
            bet.BetId,
            bet.GameId,
            bet.UserUid,
            bet.BetType,
            bet.Amount,
            bet.Number,
            bet.Color,
            bet.EvenOdd,
            bet.IsWinning,
            bet.WinningsAmount,
            bet.CreatedAt
        );
    }
}
