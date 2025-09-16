using GameRouletteBackend.IAM.Domain.Model.Commands;
using GameRouletteBackend.IAM.Domain.Model.ValueObjects;

namespace GameRouletteBackend.IAM.Domain.Model.Aggregates;

public partial class Account
{
    // Constructor principal
    public Account(
        Guid uid,
        string name,
        string role = UserRole.PLAYER,
        string status = UserStatus.ACTIVE)
    {
        Uid = uid;
        Name = UserName.Validate(name);
        Role = UserRole.Validate(role);
        Status = UserStatus.Validate(status);
        CreatedAt = DateTime.UtcNow;
        UpdatedAt = DateTime.UtcNow;
    }

    // Constructor desde comando SignUp
    public Account(SignUpCommand command) : this(
        command.Uid,
        command.Name
    ) { }

    // Propiedades con private set
    public int Id { get; private set; }
    public Guid Uid { get; private set; }
    public string Name { get; private set; }
    public string Role { get; private set; }
    public string Status { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    
    // Navegación a User (relación 1:1)
    public User? User { get; private set; }

    // Métodos de negocio
    public void SetUser(User user)
    {
        User = user;
        UpdatedAt = DateTime.UtcNow;
    }


    public void ChangeStatus(string newStatus)
    {
        Status = UserStatus.Validate(newStatus);
        UpdatedAt = DateTime.UtcNow;
    }

    public bool IsActive()
    {
        return Status == UserStatus.ACTIVE;
    }

    public bool IsPlayer()
    {
        return Role == UserRole.PLAYER;
    }
}

