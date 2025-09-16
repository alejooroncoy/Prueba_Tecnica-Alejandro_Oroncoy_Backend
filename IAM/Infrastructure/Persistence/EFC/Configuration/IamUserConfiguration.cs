using GameRouletteBackend.IAM.Domain.Model.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GameRouletteBackend.IAM.Infrastructure.Persistence.EFC.Configuration;

public static class AccountConfiguration
{
    public static void ConfigureAccount(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Uid).IsRequired();
            entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Role).IsRequired().HasMaxLength(20);
            entity.Property(e => e.Status).IsRequired().HasMaxLength(20);
            entity.Property(e => e.CreatedAt).IsRequired();
            entity.Property(e => e.UpdatedAt).IsRequired();
            
            // Configurar índices
            entity.HasIndex(e => e.Uid).IsUnique();
            entity.HasIndex(e => e.Name).IsUnique();
            entity.HasIndex(e => e.Role);
            entity.HasIndex(e => e.Status);
            
            // Configurar tabla
            entity.ToTable("Accounts");
        });
    }
}
