namespace GameRouletteBackend.IAM.Domain.Model.Commands;

public record UpdateBalanceCommand(
    Guid UserUid,
    decimal Amount
);
