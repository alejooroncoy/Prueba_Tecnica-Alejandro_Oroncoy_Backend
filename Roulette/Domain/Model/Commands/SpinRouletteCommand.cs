namespace GameRouletteBackend.Roulette.Domain.Model.Commands;

public record SpinRouletteCommand(
    Guid GameId
);
