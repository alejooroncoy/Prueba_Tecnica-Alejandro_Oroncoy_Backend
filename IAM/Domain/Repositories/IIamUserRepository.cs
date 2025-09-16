using GameRouletteBackend.IAM.Domain.Model.Aggregates;

namespace GameRouletteBackend.IAM.Domain.Repositories;

public interface IAccountRepository
{
    Task<Account?> FindByUidAsync(Guid uid);
    Task<Account?> FindByNameAsync(string name);
    Task<IEnumerable<Account>> GetAllAsync();
    Task AddAsync(Account account);
    void Update(Account account);
    void Remove(Account account);
}
