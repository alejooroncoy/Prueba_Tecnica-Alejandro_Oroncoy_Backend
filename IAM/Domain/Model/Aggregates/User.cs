using GameRouletteBackend.IAM.Domain.Model.Commands;

namespace GameRouletteBackend.IAM.Domain.Model.Aggregates;

public partial class User
{
    // Constructor principal
    public User(
        Guid uid,
        string name,
        decimal balance,
        Guid accountUid)
    {
        Uid = uid;
        Name = UserName.Validate(name);
        Balance = balance;
        AccountUid = accountUid;
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Constructor desde comando SignUp
    public User(SignUpCommand command, Guid accountUid) : this(
        Guid.NewGuid(),
        command.Name,
        command.InitialBalance,
        accountUid
    ) { }

    // Propiedades con private set
    public int Id { get; private set; }
    public Guid Uid { get; private set; }
    public string Name { get; private set; }
    public decimal Balance { get; private set; }
    public Guid AccountUid { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    // Métodos de negocio
    public void AddBalance(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("No se puede agregar un monto negativo");
            
        Balance += amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SubtractBalance(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("No se puede restar un monto negativo");
            
        if (Balance < amount)
            throw new ArgumentException("Balance insuficiente");
            
        Balance -= amount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void UpdateBalance(decimal newAmount)
    {
        if (newAmount < 0)
            throw new ArgumentException("El balance no puede ser negativo");
            
        Balance = newAmount;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetBalance(decimal newBalance)
    {
        if (newBalance < 0)
            throw new ArgumentException("El balance no puede ser negativo");
            
        Balance = newBalance;
        UpdatedAt = DateTime.UtcNow;
    }
}

// Value Object para validación de nombre de usuario
public static class UserName
{
    public static string Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("El nombre de usuario no puede estar vacío");
            
        var trimmedName = name.Trim().ToLowerInvariant();
        
        if (trimmedName.Length < 3)
            throw new ArgumentException("El nombre de usuario debe tener al menos 3 caracteres");
            
        if (trimmedName.Length > 50)
            throw new ArgumentException("El nombre de usuario no puede tener más de 50 caracteres");
            
        if (!trimmedName.All(c => char.IsLetterOrDigit(c) || c == '_' || c == '-'))
            throw new ArgumentException("El nombre de usuario solo puede contener letras, números, guiones y guiones bajos");
            
        return trimmedName;
    }
}
