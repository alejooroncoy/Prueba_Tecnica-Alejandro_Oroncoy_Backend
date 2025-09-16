namespace GameRouletteBackend.Roulette.Domain.Model.Queries;

public record GetAllBetsByGameQuery(
    Guid GameId
);
