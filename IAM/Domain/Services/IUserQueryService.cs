using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using GameRouletteBackend.IAM.Domain.Model.Queries;

namespace GameRouletteBackend.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<User?> Handle(GetUserByUidQuery query);
    Task<User?> Handle(GetUserByNameQuery query);
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByAccountUidQuery query);
}
