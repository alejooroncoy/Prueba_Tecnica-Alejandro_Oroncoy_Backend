namespace GameRouletteBackend.IAM.Interfaces.REST.Resources;

public record AuthenticatedUserResource(
    Guid Uid,
    string Name,
    decimal Balance,
    string Role,
    string Status,
    string Token
);
