using GameRouletteBackend.Roulette.Domain.Model.Commands;
using GameRouletteBackend.Roulette.Interfaces.REST.Resources;

namespace GameRouletteBackend.Roulette.Interfaces.REST.Transform;

public static class PlaceBetCommandFromResourceAssembler
{
    public static PlaceBetCommand ToCommandFromResource(PlaceBetResource resource)
    {
        return new PlaceBetCommand(
            Guid.NewGuid(), // Generar BetId
            resource.GameId,
            resource.UserUid,
            resource.BetType,
            resource.Amount,
            resource.Number,
            resource.Color,
            resource.EvenOdd
        );
    }
}
