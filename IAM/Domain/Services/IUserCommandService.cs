using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Model.Commands;

namespace GameRouletteBackend.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<User> Handle(CreateUserCommand command);
    Task Handle(UpdateUserBalanceCommand command);
}
