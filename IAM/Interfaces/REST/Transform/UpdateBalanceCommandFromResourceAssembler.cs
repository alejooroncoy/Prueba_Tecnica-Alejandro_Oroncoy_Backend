using GameRouletteBackend.IAM.Domain.Model.Commands;
using GameRouletteBackend.IAM.Interfaces.REST.Resources;

namespace GameRouletteBackend.IAM.Interfaces.REST.Transform;

public static class UpdateBalanceCommandFromResourceAssembler
{
    public static UpdateUserBalanceCommand ToCommandFromResource(Guid userUid, UpdateBalanceResource resource)
    {
        return new UpdateUserBalanceCommand(
            userUid,
            resource.Amount
        );
    }
}
