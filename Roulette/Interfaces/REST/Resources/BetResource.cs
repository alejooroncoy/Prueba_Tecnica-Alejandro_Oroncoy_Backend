namespace GameRouletteBackend.Roulette.Interfaces.REST.Resources;

public record BetResource(
    Guid BetId,
    Guid GameId,
    Guid UserUid,
    string BetType,
    decimal Amount,
    int? Number,
    string? Color,
    string? EvenOdd,
    bool? IsWinning,
    decimal? WinningsAmount,
    DateTimeOffset CreatedAt
);
