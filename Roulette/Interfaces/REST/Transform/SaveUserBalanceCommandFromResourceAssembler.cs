using GameRouletteBackend.Roulette.Domain.Model.Commands;
using GameRouletteBackend.Roulette.Interfaces.REST.Resources;

namespace GameRouletteBackend.Roulette.Interfaces.REST.Transform;

public static class SaveUserBalanceCommandFromResourceAssembler
{
    public static SaveUserBalanceCommand ToCommandFromResource(SaveUserBalanceResource resource)
    {
        return new SaveUserBalanceCommand(
            resource.UserName,
            resource.Amount
        );
    }
}
