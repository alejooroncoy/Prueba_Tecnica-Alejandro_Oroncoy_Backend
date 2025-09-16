namespace GameRouletteBackend.Roulette.Domain.Model.Commands;

public record PlaceBetCommand(
    Guid BetId,
    Guid GameId,
    Guid UserUid,
    string BetType,
    decimal Amount,
    int? Number = null,
    string? Color = null,
    string? EvenOdd = null
);
