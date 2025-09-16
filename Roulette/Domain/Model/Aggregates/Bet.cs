using GameRouletteBackend.Roulette.Domain.Model.Commands;
using GameRouletteBackend.Roulette.Domain.Model.ValueObjects;

namespace GameRouletteBackend.Roulette.Domain.Model.Aggregates;

public partial class Bet
{
    public Bet(Guid betId, Guid gameId, Guid userUid, string betType, decimal amount, int? number = null, string? color = null, string? evenOdd = null)
    {
        if (userUid == Guid.Empty)
            throw new ArgumentException("El UID del usuario no puede estar vacío");
            
        if (amount <= 0)
            throw new ArgumentException("El monto de la apuesta debe ser mayor a 0");
            
        BetId = betId;
        GameId = gameId;
        UserUid = userUid;
        BetType = BetTypeValue.Validate(betType);
        Amount = amount;
        Number = number;
        Color = color != null ? RouletteColorValue.Validate(color) : null;
        EvenOdd = evenOdd != null ? EvenOddValue.Validate(evenOdd) : null;
        IsWinning = null;
        WinningsAmount = null;
        CreatedAt = DateTimeOffset.UtcNow;
        
        ValidateBet();
    }

    public Bet(PlaceBetCommand command) : this(
        command.BetId,
        command.GameId,
        command.UserUid,
        command.BetType,
        command.Amount,
        command.Number,
        command.Color,
        command.EvenOdd
    ) { }

    private void ValidateBet()
    {
        switch (BetType)
        {
            case BetTypeValue.COLOR:
                if (Color == null)
                    throw new ArgumentException("El color es requerido para apuestas de color");
                break;
            case BetTypeValue.NUMBER:
                if (Number == null)
                    throw new ArgumentException("El número es requerido para apuestas de número");
                break;
            case BetTypeValue.EVEN_ODD:
                if (EvenOdd == null)
                    throw new ArgumentException("El valor par/impar es requerido para apuestas de par/impar");
                break;
        }
    }

    public void CalculateWinnings(int winningNumber, string? winningColor)
    {
        if (IsWinning.HasValue)
            throw new InvalidOperationException("Los premios ya han sido calculados para esta apuesta");
            
        var isWin = false;
        decimal multiplier = 0;
        
        switch (BetType)
        {
            case BetTypeValue.COLOR:
                isWin = Color == winningColor;
                multiplier = isWin ? 0.5m : 0; // Gana la mitad del monto apostado
                break;
            case BetTypeValue.EVEN_ODD:
                if (winningNumber == 0)
                {
                    isWin = false;
                    multiplier = 0;
                }
                else
                {
                    var winningEvenOdd = EvenOddValue.GetEvenOddForNumber(winningNumber);
                    isWin = EvenOdd == winningEvenOdd;
                    multiplier = isWin ? 1.0m : 0; // Gana el monto apostado
                }
                break;
            case BetTypeValue.NUMBER:
                isWin = Number == winningNumber;
                multiplier = isWin ? 3.0m : 0; // Gana el triple del monto apostado
                break;
        }
        
        IsWinning = isWin;
        WinningsAmount = Amount * multiplier;
    }

    public int Id { get; }
    public Guid BetId { get; private set; }
    public Guid GameId { get; private set; }
    public Guid UserUid { get; private set; }
    public string BetType { get; private set; }
    public decimal Amount { get; private set; }
    public int? Number { get; private set; }
    public string? Color { get; private set; }
    public string? EvenOdd { get; private set; }
    public bool? IsWinning { get; private set; }
    public decimal? WinningsAmount { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
}