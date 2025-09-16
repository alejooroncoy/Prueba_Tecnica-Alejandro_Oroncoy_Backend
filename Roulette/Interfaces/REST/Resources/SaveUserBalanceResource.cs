namespace GameRouletteBackend.Roulette.Interfaces.REST.Resources;

public record SaveUserBalanceResource(
    string UserName,
    decimal Amount
);
