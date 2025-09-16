using GameRouletteBackend.Roulette.Domain.Model.Commands;
using GameRouletteBackend.Roulette.Interfaces.REST.Resources;

namespace GameRouletteBackend.Roulette.Interfaces.REST.Transform;

public static class SpinRouletteCommandFromResourceAssembler
{
    public static SpinRouletteCommand ToCommandFromResource(SpinRouletteResource resource)
    {
        return new SpinRouletteCommand(resource.GameId);
    }
}
