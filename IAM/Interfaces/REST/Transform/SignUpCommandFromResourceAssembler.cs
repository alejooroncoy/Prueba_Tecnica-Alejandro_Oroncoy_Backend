using GameRouletteBackend.IAM.Domain.Model.Commands;
using GameRouletteBackend.IAM.Interfaces.REST.Resources;

namespace GameRouletteBackend.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(
            Guid.NewGuid(),
            resource.Name,
            resource.InitialBalance
        );
    }
}
