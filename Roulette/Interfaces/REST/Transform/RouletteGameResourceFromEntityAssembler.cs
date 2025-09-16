using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Interfaces.REST.Resources;

namespace GameRouletteBackend.Roulette.Interfaces.REST.Transform;

public static class RouletteGameResourceFromEntityAssembler
{
    public static RouletteGameResource ToResourceFromEntity(RouletteGame game)
    {
        return new RouletteGameResource(
            game.GameId,
            game.WinningNumber,
            game.WinningColor,
            game.IsCompleted,
            game.CreatedAt,
            game.CompletedAt
        );
    }
}
