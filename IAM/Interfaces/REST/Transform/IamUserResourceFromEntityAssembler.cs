using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Interfaces.REST.Resources;

namespace GameRouletteBackend.IAM.Interfaces.REST.Transform;

public static class AccountResourceFromEntityAssembler
{
    public static AccountResource ToResourceFromEntity(Account entity)
    {
        return new AccountResource(
            entity.Uid,
            entity.Name,
            entity.User?.Balance ?? 0,
            entity.Role,
            entity.Status,
            entity.CreatedAt,
            entity.UpdatedAt
        );
    }
}
