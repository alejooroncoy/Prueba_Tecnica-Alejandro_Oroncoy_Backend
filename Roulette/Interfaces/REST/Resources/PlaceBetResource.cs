namespace GameRouletteBackend.Roulette.Interfaces.REST.Resources;

public record PlaceBetResource(
    Guid GameId,
    Guid UserUid,
    string BetType,
    decimal Amount,
    int? Number = null,
    string? Color = null,
    string? EvenOdd = null
);
