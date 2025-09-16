namespace GameRouletteBackend.IAM.Interfaces.ACL;

/// <summary>
/// Facade para operaciones de balance de usuario, expone solo primitivos
/// para ser usado por otros bounded contexts
/// </summary>
public interface IUserBalanceFacade
{
    /// <summary>
    /// Verifica si un usuario existe y tiene suficiente balance
    /// </summary>
    /// <param name="userUid">UID del usuario</param>
    /// <param name="amount">Monto a verificar</param>
    /// <returns>True si el usuario existe y tiene suficiente balance</returns>
    Task<bool> HasSufficientBalanceAsync(Guid userUid, decimal amount);
    
    /// <summary>
    /// Resta un monto del balance del usuario
    /// </summary>
    /// <param name="userUid">UID del usuario</param>
    /// <param name="amount">Monto a restar</param>
    /// <returns>True si la operación fue exitosa</returns>
    Task<bool> SubtractBalanceAsync(Guid userUid, decimal amount);
    
    /// <summary>
    /// Agrega un monto al balance del usuario
    /// </summary>
    /// <param name="userUid">UID del usuario</param>
    /// <param name="amount">Monto a agregar</param>
    /// <returns>True si la operación fue exitosa</returns>
    Task<bool> AddBalanceAsync(Guid userUid, decimal amount);
}
