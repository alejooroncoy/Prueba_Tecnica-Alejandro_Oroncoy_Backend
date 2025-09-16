using GameRouletteBackend.IAM.Domain.Repositories;
using GameRouletteBackend.Shared.Domain.Repositories;

namespace GameRouletteBackend.IAM.Interfaces.ACL;

public class UserBalanceFacade(
    IUserRepository userRepository,
    IUnitOfWork unitOfWork) : IUserBalanceFacade
{
    public async Task<bool> HasSufficientBalanceAsync(Guid userUid, decimal amount)
    {
        try
        {
            // Buscar el usuario por AccountUid (más lógico desde el punto de vista del cliente)
            var user = await userRepository.FindByAccountUidAsync(userUid);
            return user != null && user.Balance >= amount;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SubtractBalanceAsync(Guid userUid, decimal amount)
    {
        try
        {
            // Buscar el usuario por AccountUid (más lógico desde el punto de vista del cliente)
            var user = await userRepository.FindByAccountUidAsync(userUid);
            if (user == null)
                return false;

            user.SubtractBalance(amount);
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> AddBalanceAsync(Guid userUid, decimal amount)
    {
        try
        {
            // Buscar el usuario por AccountUid (más lógico desde el punto de vista del cliente)
            var user = await userRepository.FindByAccountUidAsync(userUid);
            if (user == null)
                return false;

            user.AddBalance(amount);
            userRepository.Update(user);
            await unitOfWork.CompleteAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
