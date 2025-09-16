namespace GameRouletteBackend.Roulette.Domain.Model.Commands;

public record CalculateWinningsCommand(
    Guid GameId,
    int WinningNumber,
    string WinningColor
);
