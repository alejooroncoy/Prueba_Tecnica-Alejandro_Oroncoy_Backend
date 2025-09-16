using System.Net.Mime;
using GameRouletteBackend.Roulette.Domain.Model.Commands;
using GameRouletteBackend.Roulette.Domain.Model.Queries;
using GameRouletteBackend.Roulette.Domain.Services;
using GameRouletteBackend.Roulette.Interfaces.REST.Resources;
using GameRouletteBackend.Roulette.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GameRouletteBackend.Roulette.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Roulette endpoints")]
public class RouletteController(
    IRouletteQueryService queryService,
    IRouletteCommandService commandService) : ControllerBase
{
    [HttpPost("spin")]
    [SwaggerOperation(Summary = "Spin the roulette wheel")]
    [SwaggerResponse(StatusCodes.Status200OK, "Roulette spun successfully", typeof(RouletteGameResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid game data")]
    public async Task<IActionResult> SpinRoulette([FromBody] SpinRouletteResource resource)
    {
        var command = SpinRouletteCommandFromResourceAssembler.ToCommandFromResource(resource);
        var game = await commandService.Handle(command);
        var gameResource = RouletteGameResourceFromEntityAssembler.ToResourceFromEntity(game);
        return Ok(gameResource);
    }

    [HttpPost("bet")]
    [SwaggerOperation(Summary = "Place a bet on the roulette")]
    [SwaggerResponse(StatusCodes.Status201Created, "Bet placed successfully", typeof(BetResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid bet data")]
    public async Task<IActionResult> PlaceBet([FromBody] PlaceBetResource resource)
    {
        var command = PlaceBetCommandFromResourceAssembler.ToCommandFromResource(resource);
        var bet = await commandService.Handle(command);
        var betResource = BetResourceFromEntityAssembler.ToResourceFromEntity(bet);
        return CreatedAtAction(nameof(GetBetById), new { betId = bet.BetId }, betResource);
    }


    [HttpGet("game/{gameId}")]
    [SwaggerOperation(Summary = "Get game by ID")]
    [SwaggerResponse(StatusCodes.Status200OK, "Game found", typeof(RouletteGameResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Game not found")]
    public async Task<IActionResult> GetGameById(Guid gameId)
    {
        var query = new GetGameByIdQuery(gameId);
        var game = await queryService.Handle(query);
        
        if (game == null)
            return NotFound();
            
        var gameResource = RouletteGameResourceFromEntityAssembler.ToResourceFromEntity(game);
        return Ok(gameResource);
    }

    [HttpGet("bet/{betId}")]
    [SwaggerOperation(Summary = "Get bet by ID")]
    [SwaggerResponse(StatusCodes.Status200OK, "Bet found", typeof(BetResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Bet not found")]
    public Task<IActionResult> GetBetById(Guid betId)
    {
        // Este endpoint necesitaría un query específico para obtener una apuesta por ID
        // Por simplicidad, retornamos NotFound por ahora
        return Task.FromResult<IActionResult>(NotFound());
    }

    [HttpGet("game/{gameId}/bets")]
    [SwaggerOperation(Summary = "Get all bets for a game")]
    [SwaggerResponse(StatusCodes.Status200OK, "Bets found", typeof(IEnumerable<BetResource>))]
    public async Task<IActionResult> GetBetsByGame(Guid gameId)
    {
        var query = new GetAllBetsByGameQuery(gameId);
        var bets = await queryService.Handle(query);
        var betResources = bets.Select(BetResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(betResources);
    }

    [HttpPost("game/{gameId}/calculate-winnings")]
    [SwaggerOperation(Summary = "Calculate winnings for all bets in a game")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Winnings calculated successfully")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Game not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Game not completed")]
    public async Task<IActionResult> CalculateWinnings(Guid gameId)
    {
        var gameQuery = new GetGameByIdQuery(gameId);
        var game = await queryService.Handle(gameQuery);
        
        if (game == null)
            return NotFound();
            
        if (!game.IsCompleted)
            return BadRequest("Game is not completed yet");
            
        var command = new CalculateWinningsCommand(gameId, game.WinningNumber!.Value, game.WinningColor!);
        await commandService.Handle(command);
        
        return NoContent();
    }
}
