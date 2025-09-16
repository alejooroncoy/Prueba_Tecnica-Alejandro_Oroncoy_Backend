using GameRouletteBackend.IAM.Domain.Model.Commands;
using GameRouletteBackend.IAM.Interfaces.REST.Resources;

namespace GameRouletteBackend.IAM.Interfaces.REST.Transform;

public static class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource resource)
    {
        return new SignInCommand(
            resource.Name
        );
    }
}
