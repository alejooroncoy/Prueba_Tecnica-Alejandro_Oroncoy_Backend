using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Model.Queries;
using GameRouletteBackend.IAM.Domain.Repositories;
using GameRouletteBackend.IAM.Domain.Services;

namespace GameRouletteBackend.IAM.Application.Internal.QueryServices;

public class AccountQueryService(IAccountRepository repository) : IAccountQueryService
{
    public async Task<Account?> Handle(GetUserByUidQuery query)
    {
        return await repository.FindByUidAsync(query.Uid);
    }

    public async Task<Account?> Handle(GetUserByNameQuery query)
    {
        return await repository.FindByNameAsync(query.Name);
    }

    public async Task<IEnumerable<Account>> Handle(GetAllUsersQuery query)
    {
        return await repository.GetAllAsync();
    }
}
