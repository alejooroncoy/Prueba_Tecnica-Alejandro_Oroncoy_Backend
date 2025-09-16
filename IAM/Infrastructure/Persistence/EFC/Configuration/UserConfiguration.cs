using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GameRouletteBackend.IAM.Infrastructure.Persistence.EFC.Configuration;

public static class UserConfiguration
{
    public static void ConfigureUser(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Uid).IsRequired();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Balance).IsRequired().HasColumnType("decimal(18,2)");
            entity.Property(e => e.AccountUid).IsRequired();
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            
            // Configurar índices
            entity.HasIndex(e => e.Uid).IsUnique();
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.AccountUid).IsUnique();
            
            // Configurar relación 1:1 con Account
            entity.HasOne<Account>()
                .WithOne(a => a.User)
                .HasForeignKey<User>(u => u.AccountUid)
                .HasPrincipalKey<Account>(a => a.Uid)
                .OnDelete(DeleteBehavior.Cascade);
            
            // Configurar tabla
            entity.ToTable("Users");
        });
    }
}
