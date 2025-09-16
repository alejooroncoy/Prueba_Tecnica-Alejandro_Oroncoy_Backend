using GameRouletteBackend.Roulette.Domain.Model.ValueObjects;

namespace GameRouletteBackend.Roulette.Domain.Model.Aggregates;

public partial class RouletteGame
{
    public RouletteGame(Guid gameId)
    {
        GameId = gameId;
        WinningNumber = null;
        WinningColor = null;
        IsCompleted = false;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void Spin()
    {
        if (IsCompleted)
            throw new InvalidOperationException("El juego ya ha sido completado");
            
        WinningNumber = RouletteNumber.GenerateRandom();
        
        if (WinningNumber == 0)
        {
            WinningColor = null; // El 0 no tiene color
        }
        else
        {
            WinningColor = RouletteColorValue.GetColorForNumber(WinningNumber.Value);
        }
        
        IsCompleted = true;
        CompletedAt = DateTimeOffset.UtcNow;
    }

    public int Id { get; }
    public Guid GameId { get; private set; }
    public int? WinningNumber { get; private set; }
    public string? WinningColor { get; private set; }
    public bool IsCompleted { get; private set; }
    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? CompletedAt { get; private set; }
}
