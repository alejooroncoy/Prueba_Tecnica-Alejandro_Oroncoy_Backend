namespace GameRouletteBackend.IAM.Domain.Model.ValueObjects;

public static class UserRole
{
    public const string PLAYER = "PLAYER";
    
    public static readonly string[] ValidRoles = { PLAYER };
    
    public static bool IsValid(string role)
    {
        return ValidRoles.Contains(role?.ToUpper());
    }
    
    public static string Validate(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("El rol del usuario no puede estar vacío");
            
        var upperRole = role.ToUpper();
        if (!IsValid(upperRole))
            throw new ArgumentException($"Rol de usuario inválido: {role}");
            
        return upperRole;
    }
}
