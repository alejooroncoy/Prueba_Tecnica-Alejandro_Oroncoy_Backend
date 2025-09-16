namespace GameRouletteBackend.IAM.Interfaces.REST.Resources;

public record AccountResource(
    Guid Uid,
    string Name,
    decimal Balance,
    string Role,
    string Status,
    DateTime CreatedAt,
    DateTime UpdatedAt
);
