using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Model.Commands;

namespace GameRouletteBackend.IAM.Domain.Services;

public interface IAccountCommandService
{
    Task<Account> Handle(SignUpCommand command);
    Task<Account?> Handle(SignInCommand command);
}
