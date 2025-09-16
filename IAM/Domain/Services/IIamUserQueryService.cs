using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Model.Queries;

namespace GameRouletteBackend.IAM.Domain.Services;

public interface IAccountQueryService
{
    Task<Account?> Handle(GetUserByUidQuery query);
    Task<Account?> Handle(GetUserByNameQuery query);
    Task<IEnumerable<Account>> Handle(GetAllUsersQuery query);
}
