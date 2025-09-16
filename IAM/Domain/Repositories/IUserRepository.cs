using GameRouletteBackend.IAM.Domain.Model.Aggregates;

namespace GameRouletteBackend.IAM.Domain.Repositories;

public interface IUserRepository
{
    Task<User?> FindByUidAsync(Guid uid);
    Task<User?> FindByNameAsync(string name);
    Task<User?> FindByAccountUidAsync(Guid accountUid);
    Task<IEnumerable<User>> GetAllAsync();
    Task AddAsync(User user);
    void Update(User user);
    void Remove(User user);
}
