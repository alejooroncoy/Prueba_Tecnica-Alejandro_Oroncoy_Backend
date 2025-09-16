namespace GameRouletteBackend.IAM.Domain.Model.Commands;

public record CreateUserCommand(
    Guid Uid,
    string Name,
    decimal InitialBalance,
    Guid AccountUid
);
