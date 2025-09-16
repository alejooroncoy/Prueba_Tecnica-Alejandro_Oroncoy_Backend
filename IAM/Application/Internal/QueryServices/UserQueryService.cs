using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Model.Queries;
using GameRouletteBackend.IAM.Domain.Repositories;
using GameRouletteBackend.IAM.Domain.Services;

namespace GameRouletteBackend.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository repository) : IUserQueryService
{
    public async Task<User?> Handle(GetUserByUidQuery query)
    {
        return await repository.FindByUidAsync(query.Uid);
    }

    public async Task<User?> Handle(GetUserByNameQuery query)
    {
        return await repository.FindByNameAsync(query.Name);
    }

    public async Task<IEnumerable<User>> Handle(GetAllUsersQuery query)
    {
        return await repository.GetAllAsync();
    }

    public async Task<User?> Handle(GetUserByAccountUidQuery query)
    {
        return await repository.FindByAccountUidAsync(query.AccountUid);
    }
}
