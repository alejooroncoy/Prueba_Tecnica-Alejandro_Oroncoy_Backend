using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Model.Commands;
using GameRouletteBackend.IAM.Domain.Repositories;
using GameRouletteBackend.IAM.Domain.Services;
using GameRouletteBackend.Shared.Domain.Repositories;

namespace GameRouletteBackend.IAM.Application.Internal.CommandServices;

public class UserCommandService(
    IUserRepository repository,
    IUnitOfWork unitOfWork)
    : IUserCommandService
{
    public async Task<User> Handle(CreateUserCommand command)
    {
        // Verificar si el usuario ya existe
        var existingUser = await repository.FindByNameAsync(command.Name);
        if (existingUser != null)
            throw new InvalidOperationException($"El usuario '{command.Name}' ya existe");

        // Crear nuevo usuario
        var user = new User(command.Uid, command.Name, command.InitialBalance, command.AccountUid);

        try
        {
            await repository.AddAsync(user);
            await unitOfWork.CompleteAsync();
            return user;
        }
        catch (Exception e)
        {
            throw new Exception($"Error creando usuario: {e.Message}");
        }
    }

    public async Task Handle(UpdateUserBalanceCommand command)
    {
        var user = await repository.FindByUidAsync(command.UserUid);
        if (user == null)
            throw new InvalidOperationException("Usuario no encontrado");

        try
        {
            if (command.Amount > 0)
                user.AddBalance(command.Amount);
            else if (command.Amount < 0)
                user.SubtractBalance(Math.Abs(command.Amount));
            else
                user.SetBalance(0);

            repository.Update(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error actualizando saldo: {e.Message}");
        }
    }
}
