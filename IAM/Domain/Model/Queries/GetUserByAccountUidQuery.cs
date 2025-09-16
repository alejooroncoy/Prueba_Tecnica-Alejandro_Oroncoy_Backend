namespace GameRouletteBackend.IAM.Domain.Model.Queries;

public record GetUserByAccountUidQuery(
    Guid AccountUid
);
