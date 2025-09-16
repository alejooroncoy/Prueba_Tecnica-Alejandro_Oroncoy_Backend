namespace GameRouletteBackend.IAM.Domain.Model.Commands;

public record SignUpCommand(
    Guid Uid,
    string Name,
    decimal InitialBalance
);
