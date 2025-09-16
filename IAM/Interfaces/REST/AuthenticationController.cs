using GameRouletteBackend.IAM.Domain.Model.Queries;
using GameRouletteBackend.IAM.Domain.Services;
using GameRouletteBackend.IAM.Interfaces.REST.Resources;
using GameRouletteBackend.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace GameRouletteBackend.IAM.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(
    IAccountQueryService accountQueryService,
    IAccountCommandService accountCommandService,
    IUserQueryService userQueryService,
    IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("signup")]
    [SwaggerOperation(Summary = "Register a new user")]
    [SwaggerResponse(StatusCodes.Status201Created, "User created successfully", typeof(AccountResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    [SwaggerResponse(StatusCodes.Status409Conflict, "User already exists")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource resource)
    {
        try
        {
            var command = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
            var account = await accountCommandService.Handle(command);
            var userResource = AccountResourceFromEntityAssembler.ToResourceFromEntity(account);
            return CreatedAtAction(nameof(GetUserByUid), new { uid = account.Uid }, userResource);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPost("signin")]
    [SwaggerOperation(Summary = "Sign in user")]
    [SwaggerResponse(StatusCodes.Status200OK, "Sign in successful", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource resource)
    {
        try
        {
            var command = SignInCommandFromResourceAssembler.ToCommandFromResource(resource);
            var account = await accountCommandService.Handle(command);
            
            if (account == null)
                return Unauthorized(new { message = "Usuario no encontrado" });

            // Por ahora, generamos un token simple (en producción usar JWT)
            var token = $"token_{account.Uid}_{DateTime.UtcNow.Ticks}";
            var authenticatedResource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(account, token);
            
            return Ok(authenticatedResource);
        }
        catch (InvalidOperationException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("{uid}")]
    [SwaggerOperation(Summary = "Get user by uid")]
    [SwaggerResponse(StatusCodes.Status200OK, "Found", typeof(AccountResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not found")]
    public async Task<IActionResult> GetUserByUid(Guid uid)
    {
        var query = new GetUserByUidQuery(uid);
        var account = await accountQueryService.Handle(query);
        
        if (account == null)
            return NotFound();
            
        var resource = AccountResourceFromEntityAssembler.ToResourceFromEntity(account);
        return Ok(resource);
    }

    [HttpGet("name/{name}")]
    [SwaggerOperation(Summary = "Get user by name")]
    [SwaggerResponse(StatusCodes.Status200OK, "Found", typeof(AccountResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Not found")]
    public async Task<IActionResult> GetUserByName(string name)
    {
        var query = new GetUserByNameQuery(name);
        var account = await accountQueryService.Handle(query);
        
        if (account == null)
            return NotFound();
            
        var resource = AccountResourceFromEntityAssembler.ToResourceFromEntity(account);
        return Ok(resource);
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all users")]
    [SwaggerResponse(StatusCodes.Status200OK, "Found", typeof(IEnumerable<AccountResource>))]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var accounts = await accountQueryService.Handle(query);
        var resources = accounts.Select(AccountResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }

    [HttpPut("{uid}/balance")]
    [SwaggerOperation(Summary = "Update user balance")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Updated")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "User not found")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid data")]
    public async Task<IActionResult> UpdateBalance(Guid uid, [FromBody] UpdateBalanceResource resource)
    {
        try
        {
            // Buscar el usuario por AccountUid (más lógico desde el punto de vista del cliente)
            var user = await userQueryService.Handle(new GetUserByAccountUidQuery(uid));
            if (user == null)
                return NotFound(new { message = "Usuario no encontrado" });

            var command = UpdateBalanceCommandFromResourceAssembler.ToCommandFromResource(user.Uid, resource);
            await userCommandService.Handle(command);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
