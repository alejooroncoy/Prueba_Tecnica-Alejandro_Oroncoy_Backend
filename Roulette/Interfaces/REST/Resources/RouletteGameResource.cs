namespace GameRouletteBackend.Roulette.Interfaces.REST.Resources;

public record RouletteGameResource(
    Guid GameId,
    int? WinningNumber,
    string? WinningColor,
    bool IsCompleted,
    DateTimeOffset CreatedAt,
    DateTimeOffset? CompletedAt
);
