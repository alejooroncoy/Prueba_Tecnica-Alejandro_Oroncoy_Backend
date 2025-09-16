namespace GameRouletteBackend.IAM.Domain.Model.Commands;

public record UpdateUserBalanceCommand(
    Guid UserUid,
    decimal Amount
);
