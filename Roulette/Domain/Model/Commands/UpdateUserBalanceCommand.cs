namespace GameRouletteBackend.Roulette.Domain.Model.Commands;

public record UpdateUserBalanceCommand(
    string UserName,
    decimal Amount
);
