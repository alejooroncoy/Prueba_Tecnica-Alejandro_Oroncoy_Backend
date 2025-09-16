using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Interfaces.REST.Resources;

namespace GameRouletteBackend.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(Account entity, string token)
    {
        return new AuthenticatedUserResource(
            entity.Uid,
            entity.Name,
            entity.User?.Balance ?? 0,
            entity.Role,
            entity.Status,
            token
        );
    }
}
