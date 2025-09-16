using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Model.Commands;
using GameRouletteBackend.IAM.Domain.Repositories;
using GameRouletteBackend.IAM.Domain.Services;
using GameRouletteBackend.Shared.Domain.Repositories;

namespace GameRouletteBackend.IAM.Application.Internal.CommandServices;

public class AccountCommandService(
    IAccountRepository accountRepository,
    IUserRepository userRepository,
    IUnitOfWork unitOfWork)
    : IAccountCommandService
{
    public async Task<Account> Handle(SignUpCommand command)
    {
        // Verificar si el usuario ya existe
        var existingAccount = await accountRepository.FindByNameAsync(command.Name);
        if (existingAccount != null)
            throw new InvalidOperationException($"El usuario '{command.Name}' ya existe");

        // Crear nuevo account
        var account = new Account(command);

        // Crear nuevo user asociado al account
        var user = new User(command, account.Uid);

        try
        {
            await accountRepository.AddAsync(account);
            await userRepository.AddAsync(user);
            
            // Establecer la relación (esto se hace automáticamente por EF Core)
            account.SetUser(user);
            
            await unitOfWork.CompleteAsync();
            return account;
        }
        catch (Exception e)
        {
            throw new Exception($"Error creando usuario: {e.Message}");
        }
    }

    public async Task<Account?> Handle(SignInCommand command)
    {
        var account = await accountRepository.FindByNameAsync(command.Name);
        if (account == null)
            return null;

        if (!account.IsActive())
            throw new InvalidOperationException("El usuario está inactivo");

        return account;
    }
}
