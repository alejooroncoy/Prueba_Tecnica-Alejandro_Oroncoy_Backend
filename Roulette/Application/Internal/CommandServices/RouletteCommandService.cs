using GameRouletteBackend.Roulette.Domain.Model.Aggregates;
using GameRouletteBackend.Roulette.Domain.Model.Commands;
using GameRouletteBackend.Roulette.Domain.Repositories;
using GameRouletteBackend.Roulette.Domain.Services;

namespace GameRouletteBackend.Roulette.Application.Internal.CommandServices;

public class RouletteCommandService(
    IRouletteGameRepository gameRepository,
    IBetRepository betRepository)
    : IRouletteCommandService
{
    public async Task<RouletteGame> Handle(SpinRouletteCommand command)
    {
        var game = new RouletteGame(command.GameId);
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
        var bet = new Bet(command);
        
        try
        {
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
        
        foreach (var bet in bets)
        {
            bet.CalculateWinnings(command.WinningNumber, command.WinningColor);
            betRepository.Update(bet);
        }
        
        try
        {
            await betRepository.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"Error calculating winnings: {e.Message}");
        }
    }
}
