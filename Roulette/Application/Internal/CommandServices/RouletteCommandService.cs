using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Domain.Model.Commands;
using GameRouletteBackend.Roulette.Domain.Repositories;
using GameRouletteBackend.Roulette.Domain.Services;
using GameRouletteBackend.IAM.Interfaces.ACL;

namespace GameRouletteBackend.Roulette.Application.Internal.CommandServices;

public class RouletteCommandService(
    IRouletteGameRepository gameRepository,
    IBetRepository betRepository,
    IUserBalanceFacade userBalanceFacade)
    : IRouletteCommandService
{
    public async Task<RouletteGame> Handle(SpinRouletteCommand command)
    {
        // Generar un nuevo GameId único en lugar de usar el del comando
        var gameId = Guid.NewGuid();
        var game = new RouletteGame(gameId);
        game.Spin();
        
        try
        {
            await gameRepository.AddAsync(game);
            await gameRepository.SaveChangesAsync();
            return game;
        }
        catch (Exception e)
        {
            throw new Exception($"Error spinning roulette: {e.Message}");
        }
    }

    public async Task<Bet> Handle(PlaceBetCommand command)
    {
        // Verificar que el usuario tiene suficiente balance usando el Facade
        var hasSufficientBalance = await userBalanceFacade.HasSufficientBalanceAsync(command.UserUid, command.Amount);
        if (!hasSufficientBalance)
            throw new InvalidOperationException("Usuario no encontrado o balance insuficiente para realizar la apuesta");
        
        var bet = new Bet(command);
        
        try
        {
            // Restar el monto de la apuesta del balance del usuario usando el Facade
            var balanceSubtracted = await userBalanceFacade.SubtractBalanceAsync(command.UserUid, command.Amount);
            if (!balanceSubtracted)
                throw new InvalidOperationException("Error al restar el balance del usuario");
            
            await betRepository.AddAsync(bet);
            await betRepository.SaveChangesAsync();
            return bet;
        }
        catch (Exception e)
        {
            throw new Exception($"Error placing bet: {e.Message}");
        }
    }


    public async Task Handle(CalculateWinningsCommand command)
    {
        var game = await gameRepository.FindByGameIdAsync(command.GameId);
        if (game == null)
            throw new Exception($"Game with ID {command.GameId} not found");
            
        if (!game.IsCompleted)
            throw new Exception("Game is not completed yet");
            
        var bets = await betRepository.FindByGameIdAsync(command.GameId);
        
        try
        {
            foreach (var bet in bets)
            {
                bet.CalculateWinnings(command.WinningNumber, command.WinningColor);
                betRepository.Update(bet);
                
                // Si la apuesta ganó, agregar las ganancias al balance del usuario usando el Facade
                if (bet.IsWinning == true && bet.WinningsAmount.HasValue && bet.WinningsAmount > 0)
                {
                    var balanceAdded = await userBalanceFacade.AddBalanceAsync(bet.UserUid, bet.WinningsAmount.Value);
                    if (!balanceAdded)
                    {
                        // Log warning pero no fallar la operación completa
                        Console.WriteLine($"Warning: No se pudo agregar las ganancias al usuario {bet.UserUid}");
                    }
                }
            }
            
            await betRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error calculating winnings: {e.Message}");
        }
    }
}
