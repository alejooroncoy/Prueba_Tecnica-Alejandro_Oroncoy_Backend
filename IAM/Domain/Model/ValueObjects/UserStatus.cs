namespace GameRouletteBackend.IAM.Domain.Model.ValueObjects;

public static class UserStatus
{
    public const string ACTIVE = "ACTIVE";
    public const string INACTIVE = "INACTIVE";
    public const string SUSPENDED = "SUSPENDED";
    
    public static readonly string[] ValidStatuses = { ACTIVE, INACTIVE, SUSPENDED };
    
    public static bool IsValid(string status)
    {
        return ValidStatuses.Contains(status?.ToUpper());
    }
    
    public static string Validate(string status)
    {
        if (string.IsNullOrWhiteSpace(status))
            throw new ArgumentException("El estado del usuario no puede estar vacío");
            
        var upperStatus = status.ToUpper();
        if (!IsValid(upperStatus))
            throw new ArgumentException($"Estado de usuario inválido: {status}");
            
        return upperStatus;
    }
}
