namespace GameRouletteBackend.Roulette.Domain.Model.Commands;

public record SaveUserBalanceCommand(
    string UserName,
    decimal Amount
);
